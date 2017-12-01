namespace CRMCore.Module.Task.Domain
{
    public enum DueType : int
    {
        AsSoonAsPossible = 1,
        Today = 2,
        Tomorrow = 4,
        ThisWeek = 8,
        NextWeek = 16,
        SometimeLater = 32,
        OnSpecificDate = 64
    }
}
