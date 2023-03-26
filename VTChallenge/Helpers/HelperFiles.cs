namespace VTChallenge.Helpers {

    public enum Folders { Tournaments = 0}

    public class HelperFiles {

        private IWebHostEnvironment hostEnvironment;

        public HelperFiles(IWebHostEnvironment hostEnvironment) {
            this.hostEnvironment = hostEnvironment;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string fileName, string host, Folders folder) {
            string carpeta = (folder == Folders.Tournaments) ? "imgs/tournaments" : "imgs/temp";
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, fileName);

            using (Stream stream = new FileStream(path, FileMode.Create)) {
                await file.CopyToAsync(stream);
                return Path.Combine(host, carpeta, fileName);
            }
        }
    }
}
