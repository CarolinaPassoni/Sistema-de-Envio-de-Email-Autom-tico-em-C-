using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace SistemaAvisoAutomatico
{
    // Classe que representa uma tarefa
    class Tarefa
    {
        public string Descricao { get; set; }
        public DateTime DataLembrete { get; set; }
        public string EmailDestino { get; set; }

        public Tarefa(string descricao, DateTime dataLembrete, string emailDestino)
        {
            Descricao = descricao;
            DataLembrete = dataLembrete;
            EmailDestino = emailDestino;
        }
    }

    // Classe responsável por enviar e-mails
    class ServicoEmail
    {
        private string smtpServidor = "smtp.mailtrap.io";
        private int porta = 587;
        private string usuario = "9a3e75f57804f1";
        private string senha = "5fa6f7af65aedd";

        public void EnviarEmail(string destinatario, string assunto, string corpo)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("passonicarol@gmail.com");
                mail.To.Add(destinatario);
                mail.Subject = assunto;
                mail.Body = corpo;

                SmtpClient smtp = new SmtpClient(smtpServidor, porta);
                smtp.Credentials = new NetworkCredential(usuario, senha);
                smtp.EnableSsl = true;

                smtp.Send(mail);
                Console.WriteLine($" E-mail enviado para {destinatario} com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
            }
        }
    }

    // Classe principal
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA DE AVISO AUTOMÁTICO ===\n");

            // Lista de tarefas
            List<Tarefa> tarefas = new List<Tarefa>
            {
                new Tarefa("Reunião com equipe de desenvolvimento", DateTime.Now.AddSeconds(10), "passonicarol@gmail.com"),
                new Tarefa("Reunião com equipe de Gerentes", DateTime.Now.AddSeconds(10), "passonicarol@gmail.com"),



            };

            ServicoEmail servicoEmail = new ServicoEmail();

            // Loop para verificar e enviar os lembretes
            while (true)
            {
                foreach (var tarefa in tarefas)
                {
                    if (DateTime.Now >= tarefa.DataLembrete)
                    {
                        string assunto = $"Lembrete de Tarefa: {tarefa.Descricao}";
                        string corpo = $"Olá! Este é um lembrete automático da sua tarefa:\n\n" +
                                       $"{tarefa.Descricao}\n\n" +
                                       $"Data do lembrete: {tarefa.DataLembrete}";

                        servicoEmail.EnviarEmail(tarefa.EmailDestino, assunto, corpo);

                        // Remove tarefa após envio
                        tarefas.Remove(tarefa);
                        break;
                    }
                }

                if (tarefas.Count == 0)
                {
                    Console.WriteLine("\nTodos os lembretes foram enviados!");
                    break;
                }

                Thread.Sleep(1000); // Espera 1 segundo entre as verificações
            }

            Console.WriteLine("\nSistema finalizado.");
        }

