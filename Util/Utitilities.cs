using BearerAuthentication;

public class Utitilities
{
    public static int GetLoggedUserId()
    {
        BearerToken bearerToken = new BearerToken(new BearerDatabaseManager());
        var activeToken = bearerToken.GetActiveToken();

        var retorno = int.Parse(activeToken.client);
        return retorno;
    }
}