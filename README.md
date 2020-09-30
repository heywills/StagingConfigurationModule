# StagingConfigurationModule
This Kentico Xperience module enables configuring Xperience staging to fine tune the creation of staging tasks.  This reduces the need to manually pick the tasks to stage during deployment, mitigating the risk of human error.

## Background
When deploying Kentico Xperience updates using the staging module, it’s almost never safe to process all staging tasks. This is because there are many object types that we rarely want to propagate from one environment to another, like environment specific settings and users.

A common approach for managing this is to maintain a list of objects to stage during the next deployment. During the deployment process, each category of staging tasks is selected and synced according to the maintained list. This is error prone, resulting in missed components that result in bugs.

Using this module, we can tune Xperience’s staging task creation and processing, so we can simply synchronize all object tasks during the deployment. This eliminates the need to track and process a list of objects. It has virtually eliminated the oops-I-forgot-to-deploy class of bugs.

## Solution
The solution is to use Kentico Xperience’s global system events to control what staging tasks are created and how they are processed. The `StagingEvents.LogTask.Before` is used to cancel the creation of staging tasks on the source server. The `StagingEvents.GetChildProcessingType.Execute` event is used to control how child objects are processed on the target server.

This module allows the use of these system events to be fully configurable, so that developers can control what is staged from one environment to another using configurations.

## Compatibility
* .NET 4.6.1 or higher
* Kentico Xperience 12.0.29 or higher

## Installation
To install, add the NuGet package, "KenticoCommunity.StagingConfigurationModule", to your CMS project and then add the web.config sections described below.

## Usage
After adding the NuGet package to your CMS project, the StagingConfigurationModule is installed. It will look for web.config sections to determine how to handle staging tasks. 

### Register Configuration Section

Add the following line to the `configSections` element of the web.config.

```
<section name="stagingConfiguration" type="KenticoCommunity.StagingConfigurationModule.Configurations.StagingConfigurationSection,KenticoCommunity.StagingConfigurationModule" />
```

### Base Configuration

Here's the configurations that I almost always want in every environment, because I rarely want users, role membership, scheduled tasks, and settings to be staged.

```
<stagingConfiguration>
  <sourceServer>
    <excludedTypes>
      <type name="cms.settingskey" />
      <type name="cms.scheduledtask" />
      <type name="cms.user" />
      <type name="cms.userculture" />
      <type name="cms.userrole" />
      <type name="cms.usersite" />
      <type name="cms.usermacroidentity" />
    </excludedTypes>
    <excludedMediaLibraries>
    </excludedMediaLibraries>
  </sourceServer>
  <targetServer>
    <excludedChildTypes>
      <childType parentType="cms.role" childType="cms.userrole"/>
    </excludedChildTypes>
  </targetServer>
</stagingConfiguration>
```

### Lower Environment Configuration

Here's the configuration I typically add to a lower environment (e.g. dev, qa, or uat), where I don't want to overwrite content or objects controlled by authors in the next environment. With this configuration, staging the following objects is prevented:

* Users
* Settings
* Role membership
* Online forms
* Marketing automation
* Email marketing newsletters and campaigns
* Categories
* Contact Groups

```
<stagingConfiguration>
  <sourceServer>
    <excludedTypes>
      <type name="cms.form" />
      <type name="cms.settingskey" />
      <type name="cms.scheduledtask" />
      <type name="cms.user" />
      <type name="cms.userculture" />
      <type name="cms.userrole" />
      <type name="cms.usersite" />
      <type name="cms.usermacroidentity" />
      <type name="ma.automationprocess" />
      <type name="newsletter.emailtemplatenewsletter" />
      <type name="newsletter.issue" />
      <type name="newsletter.newsletter" />
      <type name="om.contactgroup" />
      <type name="cms.category" />
    </excludedTypes>
    <excludedMediaLibraries>
    </excludedMediaLibraries>
  </sourceServer>
  <targetServer>
    <excludedChildTypes>
      <childType parentType="cms.role" childType="cms.userrole"/>
    </excludedChildTypes>
  </targetServer>
</stagingConfiguration>
```

### Prevent creating staging tasks for a type
The most common use of this module is to prevent changes to certain types of objects from being captured as staging tasks on a source server.  Since the creation of staging tasks is controlled on the source server, add or remove `type` elements inside the `excludedTypes` child section within the `sourceServer` section.  So, if you want to prevent marketing automation processes from being staged, add the following `type` element:

```<type name="ma.automationprocess" />```

### Prevent processing child objects

Kentico allows customizing the processing of child and binding objects on the target server. This is because Xperience doesn't generate separate staging tasks for most types of child objects and bindings. For example, "cms.userrole" types are not staged as distinct objects. Instead, they are staged as part of the "cms.role" parent object. So, if you want to deploy role updates (I almost always do), because you've updated permissions or personalizations, but you don't want to stage the user list associated each role (I almost never do), you would need to use the `StagingEvents.GetChildProcessingType.Execute` on the target server to change how the "cms.userrole" child type is processed. However, with this module installed, you simply need to add the following `childType` element to the `excludedChildTypes` child section within the `targetServer` section.

```<childType parentType="cms.role" childType="cms.userrole"/>```

### Prevent creating staging tasks for files in some media libraries

In rare cases, one may need to prevent staging media files of certain libraries. To prevent staging all media files, simply add "media.file" to the excluded types list. But to prevent files from some medial libraries and not others, add the `mediaLibrary` tag to the `excludedMediaLibraries` child section within the `sourceServer` section.  We had to do this on a project in which Xperience was used in a headless architecture. We couldn't put images that were part of our email templates on the filesystem, because Xperience wasn't accessible by the public. However, we had routing and CDN setup for all attachments and media library files accessed through Kentico's `getmedia` and `getattachment` handlers. Therefore, we wanted files that we put in a library with the code name "emailtemplateimages" staged all the way from the dev environment to production. However, we didn't want images added to the dev and qa environment in any other library to be synchronized.  Here's an example of configuring the module to exclude the files in two media file libraries:

```
  <stagingConfiguration>
    <sourceServer>
      <excludedTypes>
        <type name="cms.settingskey" />
        <type name="cms.scheduledtask" />
        <type name="cms.user" />
        <type name="cms.userculture" />
        <type name="cms.userrole" />
        <type name="cms.usersite" />
        <type name="cms.usermacroidentity" />
      </excludedTypes>
      <excludedMediaLibraries>
        <mediaLibrary code="emailimages" />
        <mediaLibrary code="downloads" />
      </excludedMediaLibraries>
    </sourceServer>
    <targetServer>
      <excludedChildTypes>
        <childType parentType="cms.role" childType="cms.userrole"/>
      </excludedChildTypes>
    </targetServer>
  </stagingConfiguration>
</configuration>
```

### Use web.config transformations for environment-specific settings

Web.config transformations is a powerful way to provide environment-specific settings.  For example, here's one that is used to change the types and media files that can be staged from an authoring environment to a live production environment:

```
<?xml version="1.0" encoding="utf-8"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <stagingConfiguration>
    <sourceServer>
      <excludedTypes xdt:Transform="Replace">
        <type name="cms.scheduledtask" />
        <type name="cms.settingskey" />
        <type name="cms.user" />
        <type name="cms.userculture" />
        <type name="cms.userrole" />
        <type name="cms.usersite" />
      </excludedTypes>
      <excludedMediaLibraries xdt:Transform="Replace">
      </excludedMediaLibraries>
    </sourceServer>
  </stagingConfiguration>
</configuration>
```


## License

This project uses a standard MIT license which can be found [here](https://github.com/heywills/StagingConfigurationModule/blob/master/license).

## Contribution

Contributions are welcome. Feel free to submit pull requests to the repo.

## Support

Please report bugs as issues in this GitHub repo.  We'll respond as soon as possible.
