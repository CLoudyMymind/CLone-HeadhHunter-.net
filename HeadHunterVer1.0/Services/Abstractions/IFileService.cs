using System.Security.Claims;
using HeadHunterVer1._0.ViewModels;

namespace HeadHunterVer1._0.Services.Abstractions;
// Для работы с файлами, обработка фото и другой логики, связанной с управлением файлами так же
// ( решил  для формирования pdf file скачать библиотеку
// iTextSharp офф документация https://github.com/itext/itextsharp
// формирование файла задумываю реализовать тоже в этом сервисе)

/// <summary>
/// для работы с сервисом файлов загрузки скичивания файла
/// </summary>
public interface IFileService
{
    Task<string> FileRegisterCheckAsync(RegisterViewModel model);
    Task<string> FileEditAsync(EditAccountProfileViewModels model);

    Task<byte[]> GeneratePdfAsync(string id, ClaimsPrincipal user);

    string ContentTypeFile();

    string GeneratePdfFileName(string id);
}