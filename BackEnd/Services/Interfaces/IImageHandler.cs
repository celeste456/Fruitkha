namespace BackEnd.Services.Interfaces
{
    public interface IImageHandler
    {
        //Manejar imagenes y dar un poco de seguridad.
        byte[] ProcessImage(IFormFile image); // Validación y conversión a byte[]
        bool IsValidFileName(string fileName); // Validación de nombres de archivo
        string GetSafeFileName(string originalFileName); // Generar nombres seguros
    }
}
