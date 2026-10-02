namespace DeutscheBank
{
    /// <summary>
    /// Type of data that forbids a funny people to chance the state of ze object
    ///</summary>
    ///<param name="amount"> summ of the transaction</param>
    ///<param name="date"> date of the transaction</param>
    ///<param name="note"> The best note </param>
    internal record Transaction(decimal amount, DateTime date, string note)
    {

    }
}
