public class TodoTask{
    public int Id {get; set;}
    // Con los últimos framework de .netCore los string puede ser nulable.
    public string? Description {get;set;}
    public DateTime? DoneWhen {get;set;}
}