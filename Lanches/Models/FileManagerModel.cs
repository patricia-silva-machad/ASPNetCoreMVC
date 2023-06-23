namespace Lanches.Models {
    public class FileManagerModel 
    {
        //da acesso a metodos e propriedades para tratar arquivos
        public FileInfo[] Files { get; set; }
        // interface que permite enviar arquivos, ter acesso a diversas informacoes do arquivo
        public IFormFile IFormFile { get; set; }
        //Lista de arquivos que quer enviar
        public List<IFormFile> IFormFiles { get; set; }
        // armazenar o nome do servidor dos arquivos
        public string PathImagesProduto { get; set; }

    }
}
