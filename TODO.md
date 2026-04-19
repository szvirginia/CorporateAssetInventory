# 2. Projekt: Corporate Asset Inventory (Feladatlista)

### 1. Fázis: Az Alapozás (Adatbázis és Modellek)
- [X] ~~*Mappaszerkezet kialakítása (`Backend` és `Frontend` mappák)*~~ [2026-03-09]
- [X] ~~*Új Web API projekt létrehozása a terminálban (`dotnet new webapi -n Backend`)*~~ [2026-03-09]
- [X] ~~*C# Modellek megírása: `Employee.cs` (Dolgozó) és `Asset.cs` (Eszköz)*~~ [2026-03-10]
- [X] ~~- [ ] Státusz Enum elkészítése (Raktáron, Kiadva, Javítás alatt)~~ [2026-04-02]
- [X] ~~*"One-to-Many" (Egy a többhöz) kapcsolat beállítása az Entity Frameworkben*~~ [2026-04-10]
- [X] ~~*XAMPP / MySQL bekötése (`appsettings.json` beállítása és csomagok telepítése)*~~ [2026-04-10]
- [X] ~~*Adatbázis felépítése (Code-First Migrations futtatása)*~~ [2026-04-10]

### 2. Fázis: Az Üzleti Logika (Backend és LINQ)
- [X] ~~*Alap CRUD végpontok megírása (Eszközök hozzáadása, módosítása, törlése)*~~ [2026-04-19]
- [ ] Kereső és szűrő végpont megírása C#-ban (LINQ használatával)
- [X] ~~*Végpontok tesztelése Swagger-ben (Sikeresen működik a szerver önmagában?)*~~ [2026-04-19]

### 3. Fázis: Az Arc (Frontend és UI)
- [ ] Adminisztrációs felület (Dashboard) felépítése HTML/CSS-ben
- [ ] Dinamikus lista betöltése JavaScripttel (GET kérés)
- [ ] Keresősáv bekötése (Ahogy gépelsz, azonnal szűrjön a listában)
- [ ] Űrlap megépítése eszközök kiadásához (Dolgozó kiválasztása legördülő menüből)

### 4. Fázis: Biztonság és Polírozás (Security)
- [ ] Backend validáció (pl. ne lehessen elmenteni eszközt szériaszám nélkül)
- [ ] Frontend validáció (Figyelmeztető hibaüzenetek a felhasználónak)
- [ ] Alapvető XSS (Cross-Site Scripting) védelem tesztelése a keresőmezőnél