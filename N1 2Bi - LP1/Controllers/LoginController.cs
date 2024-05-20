﻿using Microsoft.AspNetCore.Mvc;
using N1_2Bi___LP1.DAO;
using N1_2Bi___LP1.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace N1_2Bi___LP1.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
            
        }
        
         public IActionResult FazLogin(string email, string senha)
        {
            using (SqlConnection conexao = ConexaoBD.GetConexao())
            {
                // Query para verificar se o usuário e senha existem no banco de dados
                string query = "SELECT COUNT(*) FROM Usuarios WHERE Email = @Email AND Senha = @Senha";
                SqlCommand cmd = new SqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Senha", senha);

                int count = (int)cmd.ExecuteScalar();
                
                if (count > 0)
                {
                    HttpContext.Session.SetString("Logado", "true");
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int userId = reader.GetInt32(0);

                        // Armazenar o ID do usuário na sessão
                        HttpContext.Session.SetInt32("UserId", userId);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Erro = "Usuário ou senha inválidos!";
                    return View("Index");
                }
            }
        }
        
        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }


}
