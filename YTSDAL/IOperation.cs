namespace YTSDAL
{
    internal interface IOperation
    {
        string Delete();

        string GetAllData();

        string Read();

        string Save();

        string Update();
    }
}