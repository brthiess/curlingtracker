public class Feed
{
    public string Name {get;set;}

    public string Url {get;set;}
    public FeedType Type {get;set;}

}

public enum FeedType {Atom, RSS}