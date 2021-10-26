namespace GenericWebAPI.Models.Request
{
    public class ResponseWrapper
    {
        public System.Net.HttpStatusCode? Status { get; set; }

        public bool Succeeded
        {
            get
            {
                return Status.HasValue && (int)Status.Value >= 200 && (int)Status.Value < 300 ? true : false;
            }
        }
    }

    public class ResponseWrapper<T>
    {
        public System.Net.HttpStatusCode? Status { get; set; } = null;

        public T Content { get; set; }

        public bool Succeeded
        {
            get
            {
                return Status.HasValue && (int)Status.Value >= 200 && (int)Status.Value < 300 ? true : false;
            }
        }
    }
}
