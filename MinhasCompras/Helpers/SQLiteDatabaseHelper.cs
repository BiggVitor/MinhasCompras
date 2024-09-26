using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks;
using MinhasCompras.Models; 
using SQLite;

namespace MinhasCompras.Helpers//Criando uma coleção de classes = namespace
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path); //Inicializa a conexão
            _conn.CreateTableAsync<Produto>().Wait(); //Cria uma tabela que recebra produtos
        }

        //Metodo para inserir um produto no banco
        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        //Metodo para atualizar um produto no banco
        public Task<List<Produto>> Update(Produto p)
        {
            //Comando sql para atualizar um produto
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE id=?";
            return _conn.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Id); //Retorna o resultado do comando SQL
        }

        //Metodo para obter todos os produtos do banco
        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        //Metodo para deletar um produto
        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id); //Deleta o produto caso o id seja o passado no metodo
        }

        //Metodo para buscar produtos pela descrição
        public Task<List<Produto>> Search(string q)
        {
            //Comando SQL para encontrar os produtos q tem uma descrição passada no metodo
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE '%" + q + "%'";
            return _conn.QueryAsync<Produto>(sql); //Retorna o resultado da consulta
        }
    }
}
