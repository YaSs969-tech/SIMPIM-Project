# 🏥 SIMPIM - Sistem Informatic pentru managementul pacienților și investigațiilor medicale

## 📋 Descriere Proiect
**SIMPIM** (Sistem Informatic pentru Managementul Pacienților și Investigațiilor Medicale) este o aplicație web dezvoltată pentru automatizarea proceselor medicale în unitățile sanitare. Sistemul gestionează fluxul complet al investigațiilor medicale, de la înregistrarea pacienților până la generarea rezultatelor.

## 🎯 Scop și Obiective
- **Digitalizarea proceselor medicale
- **Reducerea erorilor umane în gestionarea comenzilor
- **Optimizarea timpilor de așteptare pentru pacienți
- **Implementarea practică a pattern-urilor de design software

## 🏗️ Arhitectură Tehnică
- **Tip:** Arhitectură monolitică stratificată
- **Backend:** .NET 8, Entity Framework Core, SQLite
- **API:** RESTful Web API
- **Frontend:** Swagger UI (documentație interactivă)

## 🔧 Pattern-uri de Design Implementate (9 GoF)

### 🎭 Behavioral Patterns
| Pattern | Scop în SIMPIM | Implementare |
|---------|----------------|--------------|
| **Command** | Încapsularea operațiilor de creare comenzi | `CreateOrderCommand`, `CommandInvoker` |
| **Observer** | Notificări automate la schimbarea stării | `EmailOrderObserver`, `LoggingOrderObserver` |
| **Strategy** | Procesare diferită pentru comenzi standard/urgente | `StandardOrderProcessingStrategy`, `UrgentOrderProcessingStrategy` |

### 🏗️ Creational Patterns
| Pattern | Scop în SIMPIM | Implementare |
|---------|----------------|--------------|
| **Abstract Factory** | Crearea familiilor de notificatori | `EmailNotificationFactory` |
| **Builder** | Construcția listelor de rezultate medicale | `InvestigationResultsBuilder` |
| **Factory Method** | Crearea obiectelor de tip comandă | `StandardOrderFactory` |

### 🏛️ Structural Patterns
| Pattern | Scop în SIMPIM | Implementare |
|---------|----------------|--------------|
| **Adapter** | Integrarea sistemelor externe de notificare | `EmailNotifierAdapter` |
| **Decorator** | Calcul dinamic al costurilor medicale | `UrgentCostDecorator`, `ReferralFreeDecorator` |
| **Facade** | Interfață simplificată pentru sistemul complex | `OrderFacade` |

## 🚀 Instalare și Rulare

### Cerințe Preliminare
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)

### Pași de Rulare
```bash
# 1. Clonează repository-ul
git clone https://github.com/TUUSERNAME/SIMPIM-Project.git

# 2. Intră în directorul proiectului
cd SIMPIM-Project

# 3. Restaurează dependențele
dotnet restore

# 4. Rulează aplicația
dotnet run
