namespace Happy.Core
{
    public abstract partial class ComponentWithId : Component
    {
        public long Id
        {
            get;
            set;
        }

        protected ComponentWithId()
        {
            Id = InstanceId;
        }

        protected ComponentWithId(long id)
        {
            Id = id;
        }
    }
}
