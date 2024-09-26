namespace MinhasCompras.Views; // Define o namespace para as views do aplicativo
using MinhasCompras.Models; // Importa o namespace das models, onde está a classe Produto

public partial class NovoProduto : ContentPage
{
    public NovoProduto()
    {
        InitializeComponent(); // Inicializa os componentes da página
    }

    // Evento acionado quando um item da barra de ferramentas é clicado
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Cria um novo objeto Produto com os dados inseridos pelo usuário
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text, // Obtém a descrição do campo de texto
                Quantidade = Convert.ToDouble(txt_quantidade.Text), // Converte a quantidade para double
                Preco = Convert.ToDouble(txt_preco.Text), // Converte o preço para double
            };

            // Insere o novo produto no banco de dados
            await App.Database.Insert(p);
            // Exibe uma mensagem de sucesso ao usuário
            await DisplayAlert("Sucesso", "Produto Adicionado", "OK");
            // Navega de volta para a página principal
            await Navigation.PushAsync(new MainPage());
        }
        catch (Exception ex)
        {
            // Em caso de erro, exibe uma mensagem 
            await DisplayAlert("Ops", ex.Message, "Fechar");
        }
    }
}
