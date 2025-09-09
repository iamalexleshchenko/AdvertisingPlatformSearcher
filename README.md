**Advertising Platforms Searcher**

**Описание проекта:**
Веб-сервис для поиска рекламных площадок по регионам. Загружайте данные о площадках из файла и находите подходящие площадки для любой локации с помощью быстрого in-memory поиска.

**Структура проекта**
AdvertisingPlatformsSearcher/
- src/AdvertisingPlatformsSearcher/          (основное приложение)
- tests/AdvertisingPlatformsSearcher.Tests/  (модульные тесты)
- AdvertisingPlatformsSearcher.sln           (solution файл)
  
**Пример тестового файла (platforms.txt)**

Яндекс.Директ:/ru
Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik
Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl
Крутая реклама:/ru/svrd

Формат: Название_площадки: локация1, локация2, локация3

**Установка и запуск**

Клонировать репозиторий:
- bash
- git clone https://github.com/YOUR_USERNAME/AdvertisingPlatformsSearcher.git
- cd AdvertisingPlatformsSearcher
  
Запустить приложение:
- bash
- dotnet run --project src/AdvertisingPlatformsSearcher
  
Запустить тесты:
- bash
- dotnet test

Приложение доступно по адресу: http://localhost:5003

Основные команды
- bash
- dotnet build          # сборка проекта
- dotnet run            # запуск приложения
- dotnet test           # запуск тестов
- dotnet clean          # очистка сборки
- dotnet restore        # восстановление пакетов

API Endpoints
POST /api/advertisingplatforms/upload - загрузка файла с площадками
GET /api/advertisingplatforms/search?location=/ru/msk - поиск площадок по локации

Требования
.NET 9.0 SDK или новее

Любой HTTP-клиент (curl, Postman, браузер)
