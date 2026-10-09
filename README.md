# Konfiguracja i budowanie projektów .NET przez CLI

Przewodnik przedstawia kompletną ścieżkę tworzenia od zera wieloprojektowej solucji w języku **C#** z poziomu wiersza poleceń, wykorzystując narzędzie `dotnet` oraz system budowania **MSBuild**.

Przykładowa solucja będzie składać się z:

* **biblioteki klas** — logika aplikacji,
* **aplikacji konsolowej** — punkt wejścia programu,
* **projektu testów jednostkowych** — testy biblioteki z wykorzystaniem MSTest.

## Spis treści

1. [Wymagania](#1-wymagania)
2. [Inicjalizacja głównej solucji](#2-inicjalizacja-głównej-solucji)
3. [Tworzenie projektów](#3-tworzenie-projektów)
4. [Dodawanie projektów do solucji](#4-dodawanie-projektów-do-solucji)
5. [Konfiguracja zależności](#5-konfiguracja-zależności)
6. [Budowanie projektu](#6-budowanie-projektu)
7. [Uruchamianie aplikacji](#7-uruchamianie-aplikacji)
8. [Uruchamianie testów](#8-uruchamianie-testów)
9. [Czyszczenie projektu](#9-czyszczenie-projektu)
10. [Przykładowa struktura projektu](#10-przykładowa-struktura-projektu)

---

## 1. Wymagania

Przed rozpoczęciem upewnij się, że masz zainstalowane **.NET SDK**.

Weryfikacja instalacji:

```bash
dotnet --version
```

Możesz również sprawdzić szczegółowe informacje o środowisku:

```bash
dotnet --info
```

---

## 2. Inicjalizacja głównej solucji

Solucja (`.sln`) jest kontenerem organizującym wiele projektów .NET.

Utwórz pustą solucję w bieżącym katalogu:

```bash
dotnet new sln -n NazwaTwojegoProjektu
```

Po wykonaniu polecenia otrzymasz plik:

```text
NazwaTwojegoProjektu.sln
```

---

## 3. Tworzenie projektów

Projekty najlepiej tworzyć w podfolderach głównego katalogu solucji.

### Biblioteka klas

Biblioteka będzie zawierała właściwą logikę aplikacji.

```bash
dotnet new classlib -n NazwaTwojegoProjektu.Lib
```

### Aplikacja konsolowa

Aplikacja konsolowa będzie punktem wejścia programu i będzie zawierała metodę `Main`.

```bash
dotnet new console -n NazwaTwojegoProjektu.App
```

### Projekt testów jednostkowych

Do testów wykorzystamy framework **MSTest**:

```bash
dotnet new mstest -n NazwaTwojegoProjektu.Tests
```

Po wykonaniu wszystkich poleceń struktura katalogów powinna wyglądać podobnie do:

```text
NazwaTwojegoProjektu/
├── NazwaTwojegoProjektu.sln
├── NazwaTwojegoProjektu.Lib/
│   └── NazwaTwojegoProjektu.Lib.csproj
├── NazwaTwojegoProjektu.App/
│   └── NazwaTwojegoProjektu.App.csproj
└── NazwaTwojegoProjektu.Tests/
    └── NazwaTwojegoProjektu.Tests.csproj
```

---

## 4. Dodawanie projektów do solucji

Utworzenie projektów nie powoduje automatycznego dodania ich do solucji.

Dodaj je za pomocą `dotnet sln`:

```bash
dotnet sln add NazwaTwojegoProjektu.Lib
dotnet sln add NazwaTwojegoProjektu.App
dotnet sln add NazwaTwojegoProjektu.Tests
```

Możesz sprawdzić, jakie projekty znajdują się w solucji:

```bash
dotnet sln list
```

---

## 5. Konfiguracja zależności

Wieloprojektowa solucja powinna mieć jasno określoną hierarchię zależności.

W naszym przypadku będzie ona wyglądała następująco:

```text
                  ┌──────────────────┐
                  │       Lib        │
                  └────────┬─────────┘
                           │
                ┌──────────┴──────────┐
                │                     │
                ▼                     ▼
        ┌───────────────┐     ┌────────────────┐
        │      App      │     │     Tests      │
        └───────────────┘     └────────────────┘
```

Biblioteka pozostaje niezależna od pozostałych projektów.

### Referencja z aplikacji do biblioteki

```bash
dotnet add NazwaTwojegoProjektu.App reference NazwaTwojegoProjektu.Lib
```

### Referencja z testów do biblioteki

```bash
dotnet add NazwaTwojegoProjektu.Tests reference NazwaTwojegoProjektu.Lib
```

Dzięki temu:

* `App` może korzystać z klas znajdujących się w `Lib`,
* `Tests` może testować kod znajdujący się w `Lib`,
* `Lib` nie musi znać ani aplikacji, ani projektu testowego.

### Usuwanie referencji

Jeżeli przypadkowo dodasz nieprawidłową zależność, możesz ją usunąć:

```bash
dotnet remove NazwaTwojegoProjektu.App reference NazwaTwojegoProjektu.Lib
```

---

## 6. Budowanie projektu

### Build w trybie Debug

Domyślnie projekt jest budowany w konfiguracji `Debug`:

```bash
dotnet build
```

Polecenie zbuduje projekty znajdujące się w bieżącej solucji.

MSBuild analizuje zależności pomiędzy projektami i odpowiednio ustala kolejność ich budowania.

### Build w trybie Release

Do przygotowania wersji przeznaczonej do publikacji można wykorzystać konfigurację `Release`:

```bash
dotnet build -c Release
```

Konfiguracja `Release` jest przeznaczona do produkcyjnego builda i zazwyczaj wykorzystuje optymalizacje kompilatora.

---

## 7. Uruchamianie aplikacji

Aby uruchomić aplikację konsolową:

```bash
dotnet run --project NazwaTwojegoProjektu.App
```

Możesz również uruchomić ją w konfiguracji `Release`:

```bash
dotnet run --project NazwaTwojegoProjektu.App -c Release
```

---

## 8. Uruchamianie testów

Wszystkie testy znajdujące się w solucji można uruchomić za pomocą:

```bash
dotnet test
```

Polecenie:

1. znajdzie projekty testowe,
2. zbuduje wymagane projekty,
3. uruchomi testy,
4. wyświetli wynik w terminalu.

Możesz również uruchomić testy tylko dla konkretnego projektu:

```bash
dotnet test NazwaTwojegoProjektu.Tests
```

---

## 9. Czyszczenie projektu

Jeżeli wystąpią problemy z artefaktami wygenerowanymi podczas kompilacji, można wyczyścić projekt:

```bash
dotnet clean
```

Polecenie usuwa wygenerowane artefakty kompilacji, m.in. zawartość katalogów:

```text
bin/
obj/
```

Po wyczyszczeniu projektu można ponownie wykonać:

```bash
dotnet build
```

---

## 10. Przykładowa struktura projektu

Po wykonaniu wszystkich opisanych kroków projekt może wyglądać następująco:

```text
NazwaTwojegoProjektu/
│
├── NazwaTwojegoProjektu.sln
│
├── NazwaTwojegoProjektu.Lib/
│   ├── Class1.cs
│   └── NazwaTwojegoProjektu.Lib.csproj
│
├── NazwaTwojegoProjektu.App/
│   ├── Program.cs
│   └── NazwaTwojegoProjektu.App.csproj
│
└── NazwaTwojegoProjektu.Tests/
    ├── UnitTest1.cs
    └── NazwaTwojegoProjektu.Tests.csproj
```

### Zależności

```text
NazwaTwojegoProjektu.App
            │
            ▼
NazwaTwojegoProjektu.Lib
            ▲
            │
NazwaTwojegoProjektu.Tests
```

---

## Pełna sekwencja poleceń

Jeżeli chcesz utworzyć całą solucję od zera, możesz wykonać kolejno:

```bash
# Utworzenie solucji
dotnet new sln -n NazwaTwojegoProjektu

# Utworzenie projektów
dotnet new classlib -n NazwaTwojegoProjektu.Lib
dotnet new console -n NazwaTwojegoProjektu.App
dotnet new mstest -n NazwaTwojegoProjektu.Tests

# Dodanie projektów do solucji
dotnet sln add NazwaTwojegoProjektu.Lib
dotnet sln add NazwaTwojegoProjektu.App
dotnet sln add NazwaTwojegoProjektu.Tests

# Dodanie referencji
dotnet add NazwaTwojegoProjektu.App reference NazwaTwojegoProjektu.Lib
dotnet add NazwaTwojegoProjektu.Tests reference NazwaTwojegoProjektu.Lib

# Budowanie
dotnet build

# Uruchomienie aplikacji
dotnet run --project NazwaTwojegoProjektu.App

# Uruchomienie testów
dotnet test

# Czyszczenie
dotnet clean
```

## Podsumowanie

Podstawowy workflow pracy z wieloprojektową solucją .NET może wyglądać następująco:

```text
Utwórz solucję
      │
      ▼
Utwórz projekty
      │
      ▼
Dodaj projekty do solucji
      │
      ▼
Skonfiguruj referencje
      │
      ▼
dotnet build
      │
      ├──────────────► dotnet run
      │
      └──────────────► dotnet test
```

Dzięki `dotnet CLI` cały proces tworzenia, budowania, testowania i uruchamiania wieloprojektowej aplikacji .NET można wykonać bezpośrednio z poziomu terminala.

# Konfiguracja klucza SSH dla GitHub

Klucz SSH pozwala na bezpieczne łączenie się z GitHubem bez konieczności podawania loginu i hasła przy każdej operacji `git push` lub `git pull`.

## 1. Generowanie klucza SSH

Otwórz terminal i wykonaj polecenie:

```bash
ssh-keygen -t ed25519 -C "twój_email@gmail.com"
```

Zastąp `twój_email@gmail.com` adresem e-mail powiązanym z Twoim kontem GitHub.

Po wykonaniu polecenia pojawią się pytania dotyczące:

* lokalizacji zapisu klucza,
* hasła (`passphrase`).

Jeżeli chcesz użyć domyślnej lokalizacji i nie potrzebujesz hasła, możesz kilkukrotnie nacisnąć **Enter**.

Domyślna lokalizacja klucza na Windowsie to:

```text
C:\Users\NazwaUzytkownika\.ssh\
```

Zostaną utworzone dwa pliki:

```text
id_ed25519       # klucz prywatny
id_ed25519.pub   # klucz publiczny
```

> **⚠️ Ważne:** nigdy nie udostępniaj pliku `id_ed25519`. Jest to Twój **klucz prywatny**. Na GitHub dodajesz wyłącznie zawartość pliku `id_ed25519.pub`.

---

## 2. Wyświetlenie klucza publicznego

Po wygenerowaniu klucza należy wyświetlić jego zawartość i skopiować ją do GitHuba.

### PowerShell

W PowerShellu możesz użyć:

```powershell
cat ~/.ssh/id_ed25519.pub
```

Alternatywnie:

```powershell
Get-Content ~/.ssh/id_ed25519.pub
```

### Windows CMD

W klasycznym Wierszu Polecenia (`cmd`) użyj:

```cmd
type %USERPROFILE%\.ssh\id_ed25519.pub
```

Polecenie wyświetli klucz publiczny w terminalu.

Powinien on wyglądać mniej więcej tak:

```text
ssh-ed25519 AAAAC3NzaC1lZDI1NTE5AAAAI... twój_email@gmail.com
```

Skopiuj **całą linię**, zaczynającą się od:

```text
ssh-ed25519
```

---

## 3. Dodanie klucza do GitHuba

1. Zaloguj się na swoje konto GitHub.

2. Przejdź do:

   **Settings → SSH and GPG keys**

3. Kliknij:

   **New SSH key**

4. W polu **Title** wpisz nazwę pozwalającą rozpoznać urządzenie, np.:

   ```text
   Mój komputer
   ```

5. W polu **Key** wklej wcześniej skopiowany klucz publiczny.

6. Kliknij **Add SSH key**.

> Do GitHuba wklejamy zawartość pliku `id_ed25519.pub`, a **nie** `id_ed25519`.

---

## 4. Sprawdzenie połączenia z GitHubem

Po dodaniu klucza możesz sprawdzić, czy SSH działa poprawnie:

```bash
ssh -T git@github.com
```

Przy pierwszym połączeniu może pojawić się pytanie o potwierdzenie autentyczności hosta. Wpisz:

```text
yes
```

Jeżeli konfiguracja jest poprawna, GitHub powinien potwierdzić, że uwierzytelnił Twoje konto.

---

## 5. Klonowanie repozytorium przez SSH

Po skonfigurowaniu SSH możesz klonować repozytoria za pomocą adresu SSH:

```bash
git clone git@github.com:USERNAME/REPOSITORY.git
```

Przykład:

```bash
git clone git@github.com:username/moj-projekt.git
```

Zamiast adresu HTTPS:

```text
https://github.com/username/moj-projekt.git
```

możesz używać:

```text
git@github.com:username/moj-projekt.git
```

---

## Podsumowanie

Cały proces wygląda następująco:

```text
1. Wygeneruj klucz
        │
        ▼
ssh-keygen -t ed25519 -C "twój_email@gmail.com"
        │
        ▼
2. Wyświetl klucz publiczny
        │
        ▼
cat ~/.ssh/id_ed25519.pub
        │
        ▼
3. Skopiuj zawartość id_ed25519.pub
        │
        ▼
4. GitHub → Settings → SSH and GPG keys
        │
        ▼
5. New SSH key
        │
        ▼
6. Wklej klucz
        │
        ▼
7. Sprawdź połączenie
        │
        ▼
ssh -T git@github.com
```

### Najważniejsza zasada

```text
id_ed25519       → 🔒 KLUCZ PRYWATNY → NIGDY NIE UDOSTĘPNIAJ
id_ed25519.pub   → 🔑 KLUCZ PUBLICZNY → DODAJ DO GITHUBA
```
