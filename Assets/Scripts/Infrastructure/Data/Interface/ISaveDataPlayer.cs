namespace Infrastructure.Data.Interface
{
    public interface ISaveDataPlayer
    {
        public void Save(DataPlayer data);
        public void Load(DataPlayer data);
    }
}