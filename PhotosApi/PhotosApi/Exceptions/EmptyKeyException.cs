namespace PhotosApi.Exceptions
{
    [Serializable]
    public class EmptyKeyException:Exception
    {
        public EmptyKeyException() : base("Api Key is Empty") 
        {

        }
    }
}
