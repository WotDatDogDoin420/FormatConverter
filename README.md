# Excel konvertor – JSON / XML

Projekt rozšiřuje existující aplikaci pro konverzi dat z Excel (XLSX) do strukturovaného výstupu o podporu **XML formátu**.

### Cíl
- zorientovat se v existujícím kódu
- rozšířit řešení o XML konverzi
- zachovat plnou funkčnost původní JSON varianty
- respektovat stávající architekturu

### Implementace
- přidána třída XmlExcelConverter (logika vychází z existujícího JsonExcelConverter)
- zachována původní datová struktura (klienti → zakázky → položky)
- volba výstupního formátu (JSON / XML) ve webové aplikaci
- původní JSON konverze zůstala beze změn

### Spuštění aplikace
Při spuštění z Visual Studia může docházet k nestabilnímu chování.  
Doporučený funkční způsob:
```bash
cd WebExcelConverter
dotnet run
