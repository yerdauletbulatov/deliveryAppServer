namespace ApplicationCore.Entities.Values.Enums
{
    public enum GeneralState
    {
        New = 1,
        Waiting = 2,
        OnReview = 3,
        PendingForHandOver = 4, //Client
        ReceivedByDriver = 5, //Client
        InProgress = 6, //moving
        Done = 7,
        Delayed = 8,
        Canceled = 9
    }
}