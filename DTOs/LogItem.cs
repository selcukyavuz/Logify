namespace Logify;

using System;
using Newtonsoft.Json;


public class LogItem
{
    [JsonProperty(PropertyName = "id")]
    public string? Id { get; set; }

    [JsonProperty(PropertyName = "message")]
    public string? Message { get; set; }

    [JsonProperty(PropertyName = "partitionKey")]
    public string? PartitionKey { get; set; }

    [JsonProperty(PropertyName = "application")]
    public string? Application { get; set; }

    public string? _ts { get; set; }

    [JsonProperty(PropertyName = "timeStamp")]
    public DateTime? TimeStamp
    { 
        get
        {
            return double.TryParse(_ts, out double ts) ? ts.UnixTimeStampToDateTimeNullable() : null;
        }
    }
    
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}

