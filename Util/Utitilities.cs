using BearerAuthentication;

public class Utitilities
{
    public static int GetLoggedUserId()
    {
        BearerToken bearerToken = new BearerToken();
        var activeToken = bearerToken.GetActiveToken();

        var retorno = int.Parse(activeToken.client);
        return retorno;
    }
}