using System;
using System.IO;

class Atividade5
{
    static void Main()
    {
        Console.Write("Digite o nome da pasta que deseja excluir (em C:\\ExemploAula): ");
        string nome = Console.ReadLine();
        string caminho = $@"C:\ExemploAula\{nome}";

        if (Directory.Exists(caminho))
        {
            Directory.Delete(caminho, true);
            Console.WriteLine("Diretório excluído com sucesso!");
        }
        else
        {
            Console.WriteLine("Essa pasta não existe!");
        }
    }
}

