using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace NNS_Reklamacije.Services
{
    public class SQLDataGet
    {
        private SqlConnection con;

        public SQLDataGet()
        {
            Podesavanja sts = PodesavanjaServices.ProcitajFajl();

            string connectionString;

            if (sts.SqlAutentifikacija)
            {
                connectionString = $"Data Source = {sts.ImeServera}; Initial Catalog = {sts.ImeBaze}; User Id = {sts.UserName}; Password = {sts.Password};";
            }
            else
            {
                connectionString = $"Data Source={sts.ImeServera}; Initial Catalog={sts.ImeBaze}; Integrated Security=TRUE";
            }
            con = new SqlConnection(connectionString);
        }

        public void NapraviTabelu()
        {
            int kol = 0;
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("select count (*) from information_schema.tables where table_name = 'Reklamacije'", con);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];
                kol = int.Parse(row[0].ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("Došlo je do greške pri povezivanju sa bazom podataka ! (0x007)");
                return;
            }

            if (kol > 0)
            {
                MessageBox.Show("Tabela vec postoji !");
                return;
            }

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(
                "CREATE TABLE Reklamacije(\r\n\t[ID] [int] PRIMARY KEY NOT NULL,\r\n\t[Kupac] [nvarchar](50) NOT NULL DEFAULT '',\r\n\t[Dobavljac] [nvarchar](50) NOT NULL DEFAULT '',\r\n\t[KupDatum] [datetime] NULL,\r\n\t[DatSlanja] [datetime] NULL,\r\n\t[DatPovratka] [datetime] NULL,\r\n\t[Napomena] [nvarchar](max) NOT NULL DEFAULT '',\r\n\t[Razresenje] [nvarchar](max) NOT NULL DEFAULT '',\r\n\t[Dodato] [datetime] NOT NULL DEFAULT GETDATE(),\r\n\t[Proizvodjac] [nvarchar](max) NOT NULL DEFAULT '',\r\n\t[NazivModela] [nvarchar](max) NOT NULL DEFAULT '',\r\n\t[OpisModela] [nvarchar](max) NOT NULL DEFAULT '',\r\n\t[Serijski] [nvarchar](max) NOT NULL DEFAULT '',\r\n\t[Prioritet] [int] NOT NULL DEFAULT 1,\r\n\t[Zavrseno] [bit] NOT NULL DEFAULT 0,\r\n    [Kontakt] [nvarchar](max) NOT NULL DEFAULT '',\r\n    [Izbrisano] [bit] NOT NULL DEFAULT 0)"
                , con);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                MessageBox.Show("Tabela uspešno napravljena.");
            }
            catch (Exception)
            {
                MessageBox.Show("Doslo je do greške pri pravljenju tabele ! (0x008)");
            }
        }

        public int GetMaxID()
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet ds = new DataSet();

                adapter.SelectCommand = new SqlCommand("select MAX(ID) from Reklamacije", con);
                adapter.Fill(ds, "INFORMACIJE");

                DataTable dt = ds.Tables["INFORMACIJE"];
                DataRow dr = dt.Rows[0];

                if (dr[0].ToString() == "") return 0;

                int curr = int.Parse(dr[0].ToString()) + 1;

                return curr;
            }
            catch (Exception)
            {
                MessageBox.Show("Došlo je do greške pri učitavanju podataka (0x006)");
                return -1;
            }
        }

        public void IzmeniReklamaciju(Reklamacija reklamacija)
        {
            try
            {
                string _kupdatum = reklamacija.KupDatum is null ? "null" : $"'{reklamacija.KupDatum.Value.ToString("MM/dd/yyyy HH:mm:ss")}:000'";
                string _datslanja = reklamacija.DatSlanja is null ? "null" : $"'{reklamacija.DatSlanja.Value.ToString("MM/dd/yyyy HH:mm:ss")}:000'";
                string _datpovratka = reklamacija.DatPovratka is null ? "null" : $"'{reklamacija.DatPovratka.Value.ToString("MM/dd/yyyy HH:mm:ss")}:000'";
                int bit = reklamacija.Zavrseno ? 1 : 0;

                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();

                string cmd =
                @$"UPDATE Reklamacije
                SET
                Kupac = N'{reklamacija.Kupac}',
                Dobavljac = N'{reklamacija.Dobavljac}',
                KupDatum = {_kupdatum},
                DatSlanja = {_datslanja},
                DatPovratka = {_datpovratka},
                Napomena = N'{reklamacija.Napomena}',
                Razresenje = N'{reklamacija.Razresenje}',
                Dodato = '{reklamacija.Dodato.ToString("MM/dd/yyyy HH:mm:ss")}',
                Proizvodjac = N'{reklamacija.Proizvodjac}',
                NazivModela = N'{reklamacija.NazivModela}',
                OpisModela = N'{reklamacija.OpisModela}',
                Serijski = N'{reklamacija.Serijski}',
                Prioritet = {reklamacija.Prioritet},
                Zavrseno = {bit},
                Kontakt = N'{reklamacija.Kontakt}'
                WHERE ID = {reklamacija.ID}";

                adapter.SelectCommand = new SqlCommand(cmd, con);
                adapter.Fill(ds);
            }
            catch (Exception)
            {
                MessageBox.Show("Došlo je do greške pri izmeni reklamacije (0x005)");
            }
        }

        public List<string> GetStringByProp(string prop)
        {
            List<string> podatci = new List<string>();
            try
            {
                string cmd = $"select distinct {prop} from Reklamacije where Izbrisano=0";
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet ds = new DataSet();

                adapter.SelectCommand = new SqlCommand(cmd, con);
                adapter.Fill(ds, "Reklamacije");
                DataTable dt = ds.Tables["Reklamacije"];
                foreach (DataRow row in dt.Rows)
                {
                    podatci.Add((string)row[0]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Došlo je do grške pri učitavanju podataka (0x004)");
            }

            return podatci;
        }

        public List<string> GetStringByProp(string prop, string where)
        {
            List<string> podatci = new List<string>();
            try
            {
                string cmd = $"select distinct {prop} from Reklamacije where {where} and Izbrisano=0";
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet ds = new DataSet();

                adapter.SelectCommand = new SqlCommand(cmd, con);
                adapter.Fill(ds, "Reklamacije");
                DataTable dt = ds.Tables["Reklamacije"];
                foreach (DataRow row in dt.Rows)
                {
                    podatci.Add((string)row[0]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Došlo je do grške pri učitavanju podataka (0x003)");
            }

            return podatci;
        }

        public void IzbrisiReklamaciju(int id)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet ds = new DataSet();

                adapter.SelectCommand = new SqlCommand($"UPDATE Reklamacije SET Izbrisano=1 where ID={id}", con);
                adapter.Fill(ds, "Reklamacije");
                //MessageBox.Show("Reklamacija uspešno izbrisana !!!");
            }
            catch (Exception)
            {
                MessageBox.Show("Reklamacija nije uspešno izbrisana (0x002)");
            }
        }

        public void UnesiReklamaciju(Reklamacija item)
        {
            try
            {
                string _kupdatum = item.KupDatum is null ? "null" : $"'{item.KupDatum.Value.ToString("MM/dd/yyyy HH:mm:ss")}:000'";
                string _datslanja = item.DatSlanja is null ? "null" : $"'{item.DatSlanja.Value.ToString("MM/dd/yyyy HH:mm:ss")}:000'";
                string _datpovratka = item.DatPovratka is null ? "null" : $"'{item.DatPovratka.Value.ToString("MM/dd/yyyy HH:mm:ss")}:000'";
                int bit = item.Zavrseno ? 1 : 0;

                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();

                adapter.SelectCommand = new SqlCommand(
                    @$"INSERT INTO Reklamacije (ID, Kupac, Dobavljac, KupDatum,
                        DatSlanja, DatPovratka, Napomena, Razresenje, Dodato, Proizvodjac,
                        NazivModela, OpisModela, Serijski, Prioritet, Zavrseno, Kontakt)

                        VALUES  ({item.ID}, 
                        N'{item.Kupac}',
                        N'{item.Dobavljac}',
                        {_kupdatum},
                        {_datslanja},
                        {_datpovratka},
                        N'{item.Napomena}',
                        N'{item.Razresenje}',
                        '{item.Dodato.ToString("MM/dd/yyyy HH:mm:ss")}:000',
                        N'{item.Proizvodjac}',
                        N'{item.NazivModela}',
                        N'{item.OpisModela}',
                        N'{item.Serijski}',
                        {item.Prioritet},
                        {bit},
                        N'{item.Kontakt}');"
                    , con);
                adapter.Fill(ds);
            }
            catch (Exception)
            {
                MessageBox.Show("Došlo je do greške pri unošenju reklamacije u bazu podataka (0x001)");
            }
        }

        public Task<List<Reklamacija>?> NapuniReklamacije()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    string condition = $"where Izbrisano = 0";

                    SqlCommand cmd = new SqlCommand($"select * from Reklamacije {condition}", con);
                    cmd.CommandTimeout = 2;
                    adapter.SelectCommand = cmd;
                    adapter.Fill(ds, "Reklamacije");
                    DataTable dt = ds.Tables["Reklamacije"];

                    List<Reklamacija> _reklamacije = new List<Reklamacija>();
                    foreach (DataRow row in dt.Rows)
                    {
                        int ID = int.Parse(row[0].ToString());
                        string Kupac = row[1].ToString();
                        string Dobavljac = row[2].ToString();
                        DateTime? KupDatum = row[3].ToString() == string.Empty ? null : DateTime.Parse(row[3].ToString());
                        DateTime? DatSlanja = row[4].ToString() == string.Empty ? null : DateTime.Parse(row[4].ToString());
                        DateTime? DatPovratka = row[5].ToString() == string.Empty ? null : DateTime.Parse(row[5].ToString());
                        string Napomena = row[6].ToString();
                        string Razresenje = row[7].ToString();
                        DateTime Dodato = DateTime.Parse(row[8].ToString());
                        string Proizvodjac = row[9].ToString();
                        string NazivModela = row[10].ToString();
                        string OpisModela = row[11].ToString();
                        string Serijski = row[12].ToString();
                        int Prioritet = row[13].ToString() == string.Empty ? 1 : int.Parse(row[13].ToString());
                        bool Zavrseno = row[14].ToString() == string.Empty ? false : row[14].ToString() == "False" ? false : true;
                        string Kontakt = row[15].ToString();
                        bool Izbrisano = row[16].ToString() == "" ? false : row[16].ToString() == "False" ? false : true;

                        _reklamacije.Add(new Reklamacija()
                        {
                            ID = ID,
                            Kupac = Kupac,
                            Dobavljac = Dobavljac,
                            KupDatum = KupDatum,
                            DatSlanja = DatSlanja,
                            DatPovratka = DatPovratka,
                            Napomena = Napomena,
                            Razresenje = Razresenje,
                            Dodato = Dodato,
                            Proizvodjac = Proizvodjac,
                            NazivModela = NazivModela,
                            OpisModela = OpisModela,
                            Serijski = Serijski,
                            Prioritet = Prioritet,
                            Zavrseno = Zavrseno,
                            Kontakt = Kontakt,
                            Izbrisano = Izbrisano
                        }
                        );
                    }
                    return _reklamacije;
                }
                catch (Exception)
                {
                    MessageBox.Show("Došlo je do greške pri učitavanju podataka (0x000)");
                    return null;
                }
            });
        }
    }
}
