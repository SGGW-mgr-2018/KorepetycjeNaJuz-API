namespace KorepetycjeNaJuz.Core.Interfaces
{
    public interface IEntityWithTypedId<Tid>
    {
        Tid Id { get; set; }
    }
}
