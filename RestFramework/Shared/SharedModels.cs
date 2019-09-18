namespace RestAPIModule.SharedModels
{
    [System.Serializable]
    public class ResponseMessage
    {

    }

    [System.Serializable]
    public class ServerResponse<T> where T : ResponseMessage
    {
        public int responseCode;
        public T responseMsg;
        public string responseInfo;
    }

    public class EmptyResponse : ResponseMessage
    {

    }

	public struct RestAPIEntry
    {
		public string key { get; private set; }
		public string value { get; private set; }

		public RestAPIEntry(object key, object value)
		{
			this.key = key.ToString();
			this.value = value.ToString();
		}
	}
}