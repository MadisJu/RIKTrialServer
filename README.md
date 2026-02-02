# RIKTrialProject - Ürituste haldamise süsteem

Käesolev projekt on veebipõhine ürituste haldamise süsteem, mis on loodud RIK Trial ülesande raames.  
Rakendus võimaldab hallata üritusi, registreerida osalejaid (isikud ja ettevõtted) ning kasutada erinevaid maksemeetodeid.

---

## Nõuded

- .NET 10 SDK  
- Visual Studio 2026 (soovituslik)  
- SQLite andmebaas (EF Core kaudu) // Luuakse automaatselt.

---

## DBDiagram
[Andmebaasi Diagram](https://dbdiagram.io/d/RIKTrial-697706ffbd82f5fce2943fc6)


---
## Projekti käivitamine

### 1. Klooni repositoorium

### 2. Ava Visual studios .sln fail

### 3. Vali "Full Deploy / Full Start" profiil ning käivita Veebileht avaneb automaatselt. `https://localhost:5001/swagger/index.html` On Api swagger. || Dockeri olemasolul jooksutada `docker compose up -d --build` root kaustas ja veebilehe ligipääs on `localhost:7000`.
!!NB On võimalik et : Full Deploy ja Docker compose ei saa samal ajal üleval hoida, portide konfliktid!!
