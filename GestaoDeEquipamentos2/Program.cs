using System.Collections;
using System.Globalization;

namespace GestaoDeEquipamentos2
{
    public class Program
    {
        static ArrayList ListIdEquipamento = new ArrayList();
        static ArrayList ListNomeEquipamento = new ArrayList();
        static ArrayList ListPrecoEquipamento = new ArrayList();
        static ArrayList ListNrSerieEquipamento = new ArrayList();
        static ArrayList ListDataFabricacaoEquipamento = new ArrayList();
        static ArrayList ListFabricanteEquipamento = new ArrayList();
        static int ContadorIdEquipamento = 111;

        static ArrayList ChamadoListId = new ArrayList();
        static ArrayList ChamadoListIdEquipamento = new ArrayList();
        static ArrayList ChamadoListTitulo = new ArrayList();
        static ArrayList ChamadoListDescricao = new ArrayList();
        static ArrayList ChamadoListDataAbertura = new ArrayList();
        static int ContadorIdChamado = 1;
        static bool Continuar = true;
        static int OpcaoMenu;
        const int SAIR = 9;

        public static void Main()
        {
            InicializarEquipamentos();

            while (OpcaoMenu != SAIR)
            {
                try
                {
                    MostrarMenu();
                }
                catch (Exception)
                {
                    MostrarMensagem("Algo deu errado! Tecle para continuar...", false);
                }
            }
        }

        #region menus

        static void MostrarMenu()
        {
            while (Continuar)
            {
                Console.Clear();
                Console.WriteLine("    --- Bem Vindo ---\n");
                Console.WriteLine("[1] Gerenciar Equipamentos");
                Console.WriteLine("[2] Gerenciar Chamados");
                Console.WriteLine("[9] Sair");

                OpcaoMenu = byte.Parse(Console.ReadLine()!);

                switch (OpcaoMenu)
                {
                    case 1: MostrarSubMenuEquipamentos(); break;
                    case 2: MostrarSubMenuChamados(); break;
                    case SAIR: Console.Clear(); Continuar = false; break;
                }
            }
        }

        static void MostrarSubMenuEquipamentos()
        {
            while (OpcaoMenu != SAIR)
            {
                Console.Clear();
                Console.WriteLine("--- Equipamentos ---\n");
                Console.WriteLine("[1] Cadastrar");
                Console.WriteLine("[2] Editar");
                Console.WriteLine("[3] Visualizar");
                Console.WriteLine("[4] Excluir");
                Console.WriteLine("[9] Voltar");

                OpcaoMenu = byte.Parse(Console.ReadLine()!);

                switch (OpcaoMenu)
                {
                    case 1: CadastrarEquipamento(); break;
                    case 2: EditarEquipamento(); break;
                    case 3: MostrarListaEquipamentos(); Console.ReadKey(); break;
                    case 4: ExcluirEquipamento(); break;
                    case SAIR: MostrarMenu(); break;
                }
            }
        }

        static void MostrarSubMenuChamados()
        {
            while (OpcaoMenu != SAIR)
            {
                Console.Clear();
                Console.WriteLine("--- Chamados ---\n");
                Console.WriteLine("[1] Cadastrar");
                Console.WriteLine("[2] Editar");
                Console.WriteLine("[3] Visualizar");
                Console.WriteLine("[4] Excluir");
                Console.WriteLine("[9] Voltar");

                OpcaoMenu = byte.Parse(Console.ReadLine()!);

                switch (OpcaoMenu)
                {
                    case 1: CadastrarChamado(); break;
                    case 2: EditarChamado(); break;
                    case 3: MostrarListaChamados(); Console.ReadKey(); break;
                    case 4: ExcluirChamado(); break;
                    case SAIR: MostrarMenu(); break;
                }
            }
        }

        #endregion

        #region equipamento

        static string[] FormularioEquipamento()
        {
            string nomeEquipamento;
            do
            {
                Console.Clear();
                Console.Write("Informe o nome do equipamento: ");
                nomeEquipamento = Console.ReadLine()!;

            } while (!ValidarNome(nomeEquipamento));

            Console.Clear();
            Console.Write("Informe o preço de aquisição: ");
            string preco = Console.ReadLine()!;

            Console.Clear();
            Console.Write("Informe o Nrº de série: ");
            string nrSerie = Console.ReadLine()!;

            string dataFabricacao;
            do
            {
                Console.Clear();
                Console.Write("Informe a data de fabricação (dd/MM/yyyy): ");
                dataFabricacao = Console.ReadLine()!;

            } while (!ValidarData(dataFabricacao));

            Console.Clear();
            Console.Write("Informe o fabricante: ");
            string fabricante = Console.ReadLine()!;

            return new string[] { nomeEquipamento, preco, nrSerie, dataFabricacao, fabricante };
        }

        static void AdicionarNaListaEquipamentos(string[] equipamento)
        {
            string id = ContadorIdEquipamento++.ToString();

            ListIdEquipamento.Add(id);
            ListNomeEquipamento.Add(equipamento[0]);
            ListPrecoEquipamento.Add(equipamento[1]);
            ListNrSerieEquipamento.Add(equipamento[2]);
            ListDataFabricacaoEquipamento.Add(equipamento[3]);
            ListFabricanteEquipamento.Add(equipamento[4]);
        }

        static void CadastrarEquipamento()
        {
            AdicionarNaListaEquipamentos(FormularioEquipamento());
            MostrarMensagem("Equipamento cadastrado com sucesso!", true);
        }

        static void MostrarListaEquipamentos()
        {
            MostrarCabecalhoTabela(true);

            string[] equipamentos = new string[6];

            for (int i = 0; i < ListIdEquipamento.Count; i++)
            {
                equipamentos[0] = $"{ListIdEquipamento[i]}";
                equipamentos[1] = $"{ListNomeEquipamento[i]}";
                equipamentos[2] = $"{ListPrecoEquipamento[i]}";
                equipamentos[3] = $"{ListNrSerieEquipamento[i]}";
                equipamentos[4] = $"{ListDataFabricacaoEquipamento[i]}";
                equipamentos[5] = $"{ListFabricanteEquipamento[i]}";

                Console.WriteLine(ObterStringFormatadaEquipamento(equipamentos));
            }
        }

        static string ObterStringFormatadaEquipamento(string[] equipamento)
        {
            return $"{equipamento[0],-5}| {equipamento[1],-30}| {equipamento[2],-12}| {equipamento[3],-15}| {equipamento[4],-20}| {equipamento[5],-30}";
        }

        static string SolicitarEquipamento(bool editar)
        {
            Console.Write($"\nInforme o id do equipamento para {(editar ? "Editar" : "Excluir")} ou tecle Enter para Voltar.\n=> ");
            return Console.ReadLine()!;
        }

        static int ObterIndexEquipamento(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
                return -2;

            for (int index = 0; index < ListIdEquipamento.Count; index++)
            {
                if (ListIdEquipamento[index]!.ToString() == id)
                {
                    return index;
                }
            }
            return -1;
        }

        static void EditarEquipamento()
        {
            MostrarListaEquipamentos();

            int index = ObterIndexEquipamento(SolicitarEquipamento(true));

            if (!VerificarEquipamentoEncontrado(index))
                return;

            if (!ConfirmarEquipamento(ObterEquipamento(index), false, true))
                return;

            EditarListasEquipamentos(index, FormularioEquipamento());

            MostrarMensagem("Equipamento editado com sucesso!", true);
        }

        static void EditarListasEquipamentos(int index, string[] formulario)
        {
            ListNomeEquipamento[index] = formulario[0];
            ListPrecoEquipamento[index] = formulario[1];
            ListNrSerieEquipamento[index] = formulario[2];
            ListDataFabricacaoEquipamento[index] = formulario[3];
            ListFabricanteEquipamento[index] = formulario[4];
        }

        static void ExcluirEquipamentoDaLista(int index)
        {
            ListIdEquipamento.RemoveAt(index);
            ListNomeEquipamento.RemoveAt(index);
            ListPrecoEquipamento.RemoveAt(index);
            ListNrSerieEquipamento.RemoveAt(index);
            ListDataFabricacaoEquipamento.RemoveAt(index);
            ListFabricanteEquipamento.RemoveAt(index);
        }

        static string[] ObterEquipamento(int index)
        {
            string[] equipamento = new string[6];

            equipamento[0] = ListIdEquipamento[index]!.ToString()!;
            equipamento[1] = ListNomeEquipamento[index]!.ToString()!;
            equipamento[2] = ListPrecoEquipamento[index]!.ToString()!;
            equipamento[3] = ListNrSerieEquipamento[index]!.ToString()!;
            equipamento[4] = ListDataFabricacaoEquipamento[index]!.ToString()!;
            equipamento[5] = ListFabricanteEquipamento[index]!.ToString()!;

            return equipamento;
        }

        static bool VerificarSePodeExcluirEquip(string id)
        {
            foreach (string item in ChamadoListIdEquipamento)
            {
                if (item == id)
                    return false;
            }
            return true;
        }

        static void ExcluirEquipamento()
        {
            MostrarListaEquipamentos();

            string idEquipamento = SolicitarEquipamento(false);

            int index = ObterIndexEquipamento(idEquipamento);

            if (!VerificarEquipamentoEncontrado(index))
                return;

            if (!VerificarSePodeExcluirEquip(idEquipamento))
            {
                MostrarMensagem("Não é possivel excluir equipamento com chamado cadastrado", false);
                return;
            }

            if (!ConfirmarEquipamento(ObterEquipamento(index), true, false))
                return;

            ExcluirEquipamentoDaLista(index);
            MostrarMensagem("Equipamento excluido com sucesso!", true);
        }

        static bool VerificarEquipamentoEncontrado(int index)
        {
            if (index == -1)
            {
                MostrarMensagem("Equipamento não cadastrado", false);
                return false;
            }
            if (index == -2)
                return false;

            return true;
        }

        static bool ConfirmarEquipamento(string[] equipamento, bool excluir, bool editar)
        {
            string opcao;
            do
            {
                Console.Clear();
                Console.WriteLine($"Confirma {(editar ? "Editar" : excluir ? "Excluir" : "Registrar Chamado para")} o Equipamento:\n\nId: {equipamento[0]} - {equipamento[1]} ?\n\n[1] Sim [2] Não\n");
                opcao = Console.ReadLine()!;

            } while (opcao != "1" && opcao != "2");

            return opcao == "1" ? true : false;
        }

        #endregion

        #region chamados

        static string[] FormularioChamado()
        {
            Console.Clear();
            string titulo;
            do
            {
                Console.WriteLine("Digite o título do chamado");
                titulo = Console.ReadLine()!;

            } while (!ValidarNome(titulo));

            Console.Clear();
            Console.WriteLine("Digite a descricao do chamado");
            string descricao = Console.ReadLine()!;

            string dataChamado;
            do
            {
                Console.Clear();
                Console.WriteLine("Digite a data de abertura do chamado");
                dataChamado = Console.ReadLine()!;

            } while (!ValidarData(dataChamado));

            return new string[] { titulo, descricao, dataChamado };
        }

        static string[] ObterEquipamentoChamado()
        {
            MostrarListaEquipamentos();

            System.Console.Write("\nInforme o Id do equipamento que deseja registrar o chamado ou tecle Enter para voltar.\n=> ");
            string idEquipamento = Console.ReadLine()!;

            int index = ObterIndexEquipamento(idEquipamento.ToString());

            if (!VerificarEquipamentoEncontrado(index))
                MostrarSubMenuChamados();

            return ObterEquipamento(index);
        }

        static string ObterDiasEmAberto(string dataChamado)
        {
            return Convert.ToInt32(DateTime.Now.Subtract(Convert.ToDateTime(dataChamado)).TotalDays).ToString();
        }

        static void CadastrarChamado()
        {
            string[] equipamento = ObterEquipamentoChamado();

            if (!ConfirmarEquipamento(equipamento, false, false))
                return;

            string[] chamado = FormularioChamado();

            string id = ContadorIdChamado++.ToString();

            ChamadoListId.Add(id);
            ChamadoListIdEquipamento.Add(equipamento[0]);
            ChamadoListTitulo.Add(chamado[0]);
            ChamadoListDescricao.Add(chamado[1]);
            ChamadoListDataAbertura.Add(chamado[2]);

            MostrarMensagem("Chamado cadastrado com sucesso!", true);
        }

        static void MostrarListaChamados()
        {
            MostrarCabecalhoTabela(false);

            string[] chamado = new string[6];

            for (int i = 0; i < ChamadoListId.Count; i++)
            {

                var equipamento = ObterEquipamento(ObterIndexEquipamento(ChamadoListIdEquipamento[i]!.ToString()!));
                string diasEmAberto = ObterDiasEmAberto(ChamadoListDataAbertura[i]!.ToString()!);

                chamado[0] = $"{ChamadoListId[i]}";
                chamado[1] = $"{equipamento[1]}";
                chamado[2] = $"{equipamento[0]}";
                chamado[3] = $"{ChamadoListTitulo[i]}";
                chamado[4] = $"{ChamadoListDataAbertura[i]}";
                chamado[5] = $"{diasEmAberto}";

                Console.WriteLine(ObterStringFormatadaChamado(chamado));
            }
        }

        static string ObterStringFormatadaChamado(string[] chamado)
        {
            return $"{chamado[0],-5}| {chamado[1],-30} {chamado[2],5} | {chamado[3],-30}| {chamado[4],-15:d}| {chamado[5]}";
        }

        static void EditarChamado()
        {
            MostrarListaChamados();

            int index = ObterIndexChamado(SolicitarIdChamado(true));

            if (!VerificarChamadoEncontrado(index))
                return;

            if (!ConfirmarChamado(ObterChamado(index), false))
                return;

            MostrarListaEquipamentos();

            string[] equipamento = ObterEquipamentoChamado();

            if (!ConfirmarEquipamento(equipamento, false, false))
                return;

            string[] formulario = FormularioChamado();

            ChamadoListIdEquipamento[index] = equipamento[0];
            ChamadoListTitulo[index] = formulario[0];
            ChamadoListDescricao[index] = formulario[1];
            ChamadoListDataAbertura[index] = formulario[2];

            MostrarMensagem("Chamado editado com sucesso!", true);
        }

        static string SolicitarIdChamado(bool editar)
        {
            Console.Write($"\nInforme o id para {(editar ? "Editar" : "Excluir")} o chamado ou tecle Enter para voltar.\n=> ");
            return Console.ReadLine()!;
        }

        static void ExcluirChamado()
        {
            MostrarListaChamados();

            int index = ObterIndexChamado(SolicitarIdChamado(false));

            if (!VerificarChamadoEncontrado(index))
                return;

            if (!ConfirmarChamado(ObterChamado(index), true))
                return;

            ExcluirChamadoDaLista(index);
            MostrarMensagem("Chamado excluido com sucesso!", true);

        }

        static string[] ObterChamado(int index)
        {
            string[] chamado = new string[5];

            chamado[0] = ChamadoListId[index]!.ToString()!;
            chamado[1] = ChamadoListIdEquipamento[index]!.ToString()!;
            chamado[2] = ChamadoListTitulo[index]!.ToString()!;
            chamado[3] = ChamadoListDescricao[index]!.ToString()!;
            chamado[4] = ChamadoListDataAbertura[index]!.ToString()!;

            return chamado;
        }

        static bool ConfirmarChamado(string[] chamado, bool excluir)
        {
            string opcao;
            do
            {
                Console.Clear();
                Console.WriteLine($"Confirma {(excluir ? "Excluir" : "Editar")} o Chamado:\n\nId: {chamado[0]} - Equipamento Id: {chamado[1]} - Título: {chamado[2]} ?\n\n[1] Sim [2] Não\n");
                opcao = Console.ReadLine()!;

            } while (opcao != "1" && opcao != "2");

            return opcao == "1" ? true : false;
        }

        static void ExcluirChamadoDaLista(int index)
        {
            ChamadoListId.RemoveAt(index);
            ChamadoListIdEquipamento.RemoveAt(index);
            ChamadoListTitulo.RemoveAt(index);
            ChamadoListDescricao.RemoveAt(index);
            ChamadoListDataAbertura.RemoveAt(index);
        }

        static int ObterIndexChamado(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
                return -2;

            for (int index = 0; index < ChamadoListId.Count; index++)
            {
                if (ChamadoListId[index]!.ToString() == id)
                {
                    return index;
                }
            }
            return -1;

        }

        static bool VerificarChamadoEncontrado(int index)
        {
            if (index == -1)
            {
                MostrarMensagem("Chamado não cadastrado", false);
                return false;
            }
            if (index == -2)
                return false;

            return true;
        }

        #endregion

        #region validacoes

        static void MostrarMensagem(string mensagem, bool sucesso)
        {
            Console.Clear();
            Console.ForegroundColor = sucesso ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(mensagem);
            Console.ResetColor();
            Console.ReadKey();
        }

        static bool ValidarData(string data)
        {
            DateTime dataValida;

            if (!DateTime.TryParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataValida))
            {
                MostrarMensagem("Data em formato inválido", false);
                return false;
            }

            else if (dataValida > DateTime.Now)
            {
                MostrarMensagem("Data Inválida", false);
                return false;
            }

            return true;
        }

        static bool ValidarNome(string nome)
        {
            if (nome.Length < 6 || nome.Length >= 25)
            {
                MostrarMensagem("O nome deve conter no mínimo 6 caracteres e no máximo 25.", false);
                return false;
            }
            return true;
        }

        static bool VerificaListaVazia(bool equipamento)
        {
            if (equipamento)
            {
                if (ListIdEquipamento.Count == 0)
                {
                    MostrarMensagem("Nenhum Equipamento Cadastrado", false);
                    return false;
                }
            }
            else
            {
                if (ChamadoListId.Count == 0)
                {
                    MostrarMensagem("Nenhum Chamado Cadastrado", false);
                    return false;
                }

            }
            return true;
        }

        static void MostrarCabecalhoTabela(bool equipamento)
        {
            Console.Clear();

            if (equipamento)
            {
                if (!VerificaListaVazia(true))
                    MostrarSubMenuEquipamentos();

                Console.WriteLine("  ID |            NOME               | PREÇO       | NR SÉRIE       | DATA FABRICAÇÃO     | FABRICANTE    ");
                Console.WriteLine("-----|-------------------------------|-------------|----------------|---------------------|----------------");
            }
            else
            {
                if (!VerificaListaVazia(false))
                    MostrarSubMenuChamados();

                Console.WriteLine("  ID | EQUIPAMENTO                       ID | TÍTULO                        | DATA ABERTURA  | DIAS EM ABERTO      ");
                Console.WriteLine("-----|--------------------------------------|-------------------------------|----------------|---------------------");
            }
        }

        #endregion

        #region inicializar equipamentos

        static void InicializarEquipamentos()
        {
            ArrayList dadosEquipamentos = new ArrayList();

            dadosEquipamentos.Add(new string[] { "Impressora Jato de Tinta Desk", "R$ 950,00", "234567890", "15/05/2023", "HP" });
            dadosEquipamentos.Add(new string[] { "Scanner de Mesa", "R$ 780,00", "345678901", "20/08/2023", "Epson" });
            dadosEquipamentos.Add(new string[] { "Notebook Dell i5", "R$ 4000,00", "456789012", "31/12/2023", "Dell" });
            dadosEquipamentos.Add(new string[] { "Mouse Óptico USB", "R$ 50,00", "567890123", "01/01/2022", "Microsoft" });
            dadosEquipamentos.Add(new string[] { "Teclado ABNT2 USB", "R$ 80,00", "678901234", "10/02/2022", "Genius" });
            dadosEquipamentos.Add(new string[] { "Webcam Full HD", "R$ 300,00", "890123456", "20/06/2022", "Logitech" });
            dadosEquipamentos.Add(new string[] { "Headset Gamer", "R$ 250,00", "789012345", "15/04/2022", "Razer" });
            dadosEquipamentos.Add(new string[] { "Webcam Full HD", "R$ 300,00", "890123456", "20/06/2021", "Logitech" });
            dadosEquipamentos.Add(new string[] { "Caixa de Som Bluetooth", "R$ 150,00", "901234567", "25/08/2020", "JBL" });
            dadosEquipamentos.Add(new string[] { "Monitor LED 24 Polegadas", "R$ 1000,00", "012345678", "30/11/2020", "Samsung" });
            dadosEquipamentos.Add(new string[] { "Estabilizador de Energia", "R$ 200,00", "123456780", "05/02/2020", "SMS" });

            foreach (string[] item in dadosEquipamentos)
            {
                AdicionarNaListaEquipamentos(item);
            }
        }

        #endregion
    }
}
