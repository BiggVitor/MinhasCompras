using System.Collections.ObjectModel;
using MinhasCompras.Models;

namespace MinhasCompras
{
    //Criando a classe mainpage
    public partial class MainPage : ContentPage
    {

        ObservableCollection<Produto> lista_produtos = new ObservableCollection<Produto>();

        public MainPage()
        {
            InitializeComponent(); //Inicializa os componentes da pagina
            lst_produtos.ItemsSource = lista_produtos;
        }

        //Evento pque é executado quando um item da barra de ferramentas é clicado
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
           
        }

        //Evento q é executado quando o texto da barra de pesquisa é alterado
        private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string q = e.NewTextValue;
            lista_produtos.Clear(); //Limpa a lista de produtos

            
            List<Produto> tmp = await App.Database.Search(q);
            foreach (Produto p in tmp)
            {
                lista_produtos.Add(p);//Adiciona um novo item a lista
            }
        }

        //Evento executado quando um item é selecionado
        private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Produto? p = e.SelectedItem as Produto;

            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = p
            });
        }

        //Evento  executado quando um item do menu é clicado
        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem selecionado = (MenuItem)sender;
                Produto p = selecionado.BindingContext as Produto;
                bool confirm = await DisplayAlert("Tem Certeza ?", "Remover Produto", "Sim", "Não"); //Eixbe uma mensagem de confirmação

                if (confirm)
                {
                    await App.Database.Delete(p.Id); //Remove o item do banco
                    await DisplayAlert("Sucesso!", "Produto Removido", "OK"); //Exibe uma mensagem de sucesso
                    lista_produtos.Remove(p); //Remove o item da lista
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "Fechar"); //Exibe uma mensagem de erro
            }
        }

        //Evento para somar
        private void ToolbarItem_Clicked_Somar(object sender, EventArgs e)
        {
            double soma = lista_produtos.Sum(i => i.Total); //Calcula o total dos itens
            string msg = $"O Total dos Produtos é {soma:C}"; //Formatando a mensagem
            DisplayAlert("Resultado", msg, "Fechar"); //Exibe uma mensagem com o resultado
        }

        //Evento q é executado quando clicar em adicionar novo produto
        private async void ToolbarItem_Clicked_Add(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.NovoProduto());
        }

        //
        protected async override void OnAppearing()
        {
            if (lista_produtos.Count == 0) //Verifica se a lista de produtos esta vazia
            {
                List<Produto> tmp = await App.Database.GetAll(); //Lista todos os itens da tabela
                foreach (Produto p in tmp)
                {
                    lista_produtos.Add(p); //Adiciona os itens a lista
                }
            }
        }
    }
}
