using System;
using Newtonsoft.Json;

namespace CourseTodo.Api
{
  public class Todo
  {
    [JsonProperty("id")]
    public Guid Id { get; set; }

    [JsonProperty("task")]
    public string Task { get; set; }

    [JsonProperty("complete")]
    public bool Complete { get; set; }
  }
}
