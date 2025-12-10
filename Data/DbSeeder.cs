using SimPim.Api.Models;

namespace SimPim.Api.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
         if (db.Investigatii.Any()) return;

        var investigatii = new List<Investigatie>
        {
            // --- Analiza generală a sângelui (CBC) ---
            new()
            {
                Cod = "CBC",
                Denumire = "Analiza generală a sângelui",
                Parametri = new List<ParametruInvestigatie>
                {
                    new() { CodParametru = "WBC", Denumire = "Leucocite",   Unitate = "10⁹/L",  ValoareMin = 4.0, ValoareMax = 11.0 },
                    new() { CodParametru = "RBC", Denumire = "Eritrocite",  Unitate = "10¹²/L", ValoareMin = 4.2, ValoareMax = 5.9 },
                    new() { CodParametru = "HGB", Denumire = "Hemoglobină", Unitate = "g/dL",   ValoareMin = 12.0, ValoareMax = 17.5 },
                    new() { CodParametru = "HCT", Denumire = "Hematocrit",  Unitate = "%",      ValoareMin = 36,   ValoareMax = 50 },
                    new() { CodParametru = "PLT", Denumire = "Trombocite",  Unitate = "10⁹/L",  ValoareMin = 150,  ValoareMax = 400 }
                }
            },

            // --- Glicemie ---
            new()
            {
                Cod = "GLU",
                Denumire = "Glicemie",
                Parametri = new List<ParametruInvestigatie>
                {
                    new() { CodParametru = "GLU", Denumire = "Glucoză", Unitate = "mg/dL", ValoareMin = 70, ValoareMax = 99 }
                }
            },

            // --- Profil lipidic ---
            new()
            {
                Cod = "LIP",
                Denumire = "Profil lipidic",
                Parametri = new List<ParametruInvestigatie>
                {
                    new() { CodParametru = "CHOL", Denumire = "Colesterol total", Unitate = "mg/dL", ValoareMax = 200 },
                    new() { CodParametru = "HDL",  Denumire = "HDL colesterol",   Unitate = "mg/dL", ValoareMin = 40 },
                    new() { CodParametru = "LDL",  Denumire = "LDL colesterol",   Unitate = "mg/dL", ValoareMax = 100 },
                    new() { CodParametru = "TG",   Denumire = "Trigliceride",     Unitate = "mg/dL", ValoareMax = 150 }
                }
            },

            // --- Panel hepatic ---
            new()
            {
                Cod = "HEP",
                Denumire = "Panel hepatic",
                Parametri = new List<ParametruInvestigatie>
                {
                    new() { CodParametru = "ALT",  Denumire = "Alanin-aminotransferază",  Unitate = "U/L",   ValoareMax = 55 },
                    new() { CodParametru = "AST",  Denumire = "Aspartat-aminotransferază", Unitate = "U/L",   ValoareMax = 48 },
                    new() { CodParametru = "TBIL", Denumire = "Bilirubină totală",         Unitate = "mg/dL", ValoareMax = 1.2 }
                }
            },

            // --- Panel renal ---
            new()
            {
                Cod = "REN",
                Denumire = "Panel renal",
                Parametri = new List<ParametruInvestigatie>
                {
                    new() { CodParametru = "UREA", Denumire = "Uree",        Unitate = "mg/dL", ValoareMin = 10, ValoareMax = 50 },
                    new() { CodParametru = "CREA", Denumire = "Creatinină",  Unitate = "mg/dL", ValoareMin = 0.6, ValoareMax = 1.3 },
                    new() { CodParametru = "URIC", Denumire = "Acid uric",   Unitate = "mg/dL", ValoareMin = 3.4, ValoareMax = 7.0 }
                }
            },

            // --- Analiza urinei ---
            new()
            {
                Cod = "URI",
                Denumire = "Analiza urinei",
                Parametri = new List<ParametruInvestigatie>
                {
                    new() { CodParametru = "PH",   Denumire = "pH urină",         Unitate = "-",      ValoareMin = 4.5, ValoareMax = 8.0 },
                    new() { CodParametru = "PROT", Denumire = "Proteine",         Unitate = "mg/dL",  ValoareMax = 150 },
                    new() { CodParametru = "GLU",  Denumire = "Glucoză urinară",  Unitate = "mg/dL",  ValoareMax = 15 }
                }
            },

            // --- Profil tiroidian ---
            new()
            {
                Cod = "THY",
                Denumire = "Profil tiroidian",
                Parametri = new List<ParametruInvestigatie>
                {
                    new() { CodParametru = "TSH", Denumire = "Hormon TSH", Unitate = "µIU/mL", ValoareMin = 0.4, ValoareMax = 4.0 },
                    new() { CodParametru = "FT4", Denumire = "Tiroxină liberă (FT4)", Unitate = "ng/dL", ValoareMin = 0.8, ValoareMax = 1.8 },
                    new() { CodParametru = "FT3", Denumire = "Triiodotironină (FT3)", Unitate = "pg/mL", ValoareMin = 2.3, ValoareMax = 4.2 }
                }
            },

            // --- Electroliți serici ---
            new()
            {
                Cod = "ELEC",
                Denumire = "Electroliți serici",
                Parametri = new List<ParametruInvestigatie>
                {
                    new() { CodParametru = "NA", Denumire = "Sodiu",   Unitate = "mmol/L", ValoareMin = 135, ValoareMax = 145 },
                    new() { CodParametru = "K",  Denumire = "Potasiu", Unitate = "mmol/L", ValoareMin = 3.5, ValoareMax = 5.1 },
                    new() { CodParametru = "CL", Denumire = "Clor",    Unitate = "mmol/L", ValoareMin = 98,  ValoareMax = 107 }
                }
            },

            // --- Panel imunologic ---
            new()
            {
                Cod = "IMM",
                Denumire = "Panel imunologic",
                Parametri = new List<ParametruInvestigatie>
                {
                    new() { CodParametru = "CRP", Denumire = "Proteina C reactivă", Unitate = "mg/L",  ValoareMax = 5 },
                    new() { CodParametru = "ANA", Denumire = "Anticorpi antinucleari (ANA)", Unitate = "-", ValoareMax = 1 },
                    new() { CodParametru = "RF",  Denumire = "Factor reumatoid",   Unitate = "IU/mL", ValoareMax = 14 }
                }
            },
        
            // --- Panel hormonal feminin ---
        new()
        {
            Cod = "HORF",
            Denumire = "Panel hormonal feminin",
            Parametri = new List<ParametruInvestigatie>
            {
                new() { CodParametru = "ESTR", Denumire = "Estradiol (E2)", Unitate = "pg/mL", ValoareMin = 30, ValoareMax = 400 },
                new() { CodParametru = "PROG", Denumire = "Progesteron", Unitate = "ng/mL", ValoareMin = 0.2, ValoareMax = 25 },
                new() { CodParametru = "LH",   Denumire = "Hormon luteinizant (LH)", Unitate = "mIU/mL", ValoareMin = 2, ValoareMax = 12 },
                new() { CodParametru = "FSH",  Denumire = "Hormon foliculostimulant (FSH)", Unitate = "mIU/mL", ValoareMin = 3, ValoareMax = 15 }
            }
        }

        };

        db.Investigatii.AddRange(investigatii);
        db.SaveChanges();
    }
}
