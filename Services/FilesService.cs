using FileStorage.DBContext;
using FileStorage.Core.Entities;
using File = FileStorage.Core.Entities.File;
using FileStorage.Core.DTO;

namespace FileStorage.Services
{
    public class FilesService(FileStorageContext context)
    {
        public async Task<long> Insert(IFormFile formFile, string fileName, string userEmail)
        {
            using MemoryStream memoryStream = new();

            await formFile.CopyToAsync(memoryStream);

            string uploadedFileBase64String = Convert.ToBase64String(memoryStream.ToArray());

            File file = new()
            {
                CreatedAt = DateTime.UtcNow,
                Filename = fileName,
                FileBase64 = uploadedFileBase64String
            };

            try
            {
                context.Add(file);

                context.UserFileUploads.Add(new UserFileUpload
                {
                    FileId = file.Id,
                    UserId = userEmail,
                    File = file
                });

                await context.SaveChangesAsync();

                return file.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return default;
            }
        }

        public async Task<FileDTO?> Get(int fileId)
        {
            return await GetFile(fileId);
        }

        public async Task<FileDTO?> Get(int fileId, string userEmail)
        {
            return context.UserFileUploads.Where(userFile =>
                                                 userFile.FileId == fileId &&
                                                 userFile.UserId == userEmail).Any()
                                                 ?
                                                 await GetFile(fileId) : default;
        }

        public string GetFileName(string fileName)
        {
            return Path.GetFileName(fileName);
        }

        private async Task<FileDTO?> GetFile(int id)
        {
            File? file = context.Files.Where(file => file.Id == id).FirstOrDefault();

            if (file is null) return default;

            try
            {
                byte[] fileBytes = Convert.FromBase64String(file.FileBase64);

                file.UpdatedAt = DateTime.UtcNow;

                await context.SaveChangesAsync();

                return new FileDTO(fileBytes, file.Filename);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return default;
            }
        }
    }
}