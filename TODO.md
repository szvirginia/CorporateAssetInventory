# 2. Projekt: Corporate Asset Inventory (Feladatlista)

### 1. Fázis: Az Alapozás (Adatbázis és Modellek)
- [X] ~~*Mappaszerkezet kialakítása (`Backend` és `Frontend` mappák)*~~ [2026-03-09]
- [ ] Új Web API projekt létrehozása a terminálban (`dotnet new webapi -n Backend`)
- [ ] C# Modellek megírása: `Employee.cs` (Dolgozó) és `Asset.cs` (Eszköz)
- [ ] Státusz Enum elkészítése (Raktáron, Kiadva, Javítás alatt)
- [ ] "One-to-Many" (Egy a többhöz) kapcsolat beállítása az Entity Frameworkben
- [ ] XAMPP / MySQL bekötése (`appsettings.json` beállítása és csomagok telepítése)
- [ ] Adatbázis felépítése (Code-First Migrations futtatása)

### 2. Fázis: Az Üzleti Logika (Backend és LINQ)
- [ ] Alap CRUD végpontok megírása (Eszközök hozzáadása, módosítása, törlése)
- [ ] Kereső és szűrő végpont megírása C#-ban (LINQ használatával)
- [ ] Végpontok tesztelése Swagger-ben (Sikeresen működik a szerver önmagában?)

### 3. Fázis: Az Arc (Frontend és UI)
- [ ] Adminisztrációs felület (Dashboard) felépítése HTML/CSS-ben
- [ ] Dinamikus lista betöltése JavaScripttel (GET kérés)
- [ ] Keresősáv bekötése (Ahogy gépelsz, azonnal szűrjön a listában)
- [ ] Űrlap megépítése eszközök kiadásához (Dolgozó kiválasztása legördülő menüből)

### 4. Fázis: Biztonság és Polírozás (Security)
- [ ] Backend validáció (pl. ne lehessen elmenteni eszközt szériaszám nélkül)
- [ ] Frontend validáció (Figyelmeztető hibaüzenetek a felhasználónak)
- [ ] Alapvető XSS (Cross-Site Scripting) védelem tesztelése a keresőmezőnél