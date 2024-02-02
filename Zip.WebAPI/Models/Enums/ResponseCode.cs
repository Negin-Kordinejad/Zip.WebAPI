namespace Zip.WebAPI.Models.Enums
{
    public enum ResponseCode
    {
        InternalError = 501,
        ValidationError = 400,
        BadRequest = 400,
        UnAuthorized = 401,
        Created = 201,
        Ok = 200,
        NotFound = 404
    }
}
