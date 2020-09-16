namespace KenticoCommunity.StagingConfigurationModule.Models
{
    /// <summary>
    /// Hold a Kentico Xperience parent/child object type relationship. For example, when a staging task for a cms.role is
    /// created it automatically includes its list of cms.userrole child objects. To reference that relationship, the ParentType
    /// would be "cms.role" and the child type would be "cms.userrole".
    /// </summary>
    public class ParentChildTypePair
    {
        public string ChildType;
        public string ParentType;
    }
}