# Vitura Health Prescription Management

## Overview
This project is a full-stack application for managing patient prescriptions, built with ASP.NET Core (.NET 8) for the backend and React (Vite) for the frontend.

## Backend (ASP.NET Core)
- **Runs on HTTP, port 5299** by default.
- **JSON data is loaded once at startup** and cached in memory for fast access.
- **Configuration** is managed via `appsettings.json` and DI.
- **CORS** is configured to allow requests from `http://localhost:5299`.
- **API Documentation** is available via Swagger at `/swagger`.

## Frontend (React + Vite)
- **Runs on HTTP, port 5300** by default.
- **API base URL** is configurable via the `VITE_API_BASE` environment variable (defaults to `http://localhost:5300/api`).
- **No HTTPS or certificate setup required**.
---

## 1. How to Run Both Frontend and Backend
If you have visual studio installed, then the solution has been preconfigured to run both the server and client at startup. Simply run (play button) the http debug session and follow your node. Otherwise ...

### To Run the Server / Backend:
1. **Configuration**  
   - Ensure `appsettings.json` is present and correctly configured for your environment.
   - Data is loaded from JSON files in the `/Data` directory (see `JsonDataStoreConfig`).
   - Ensure `appsettings.json` contains correct paths for patient and prescription JSON files and decide if you want to persist the prescription changes or not with the 'persistChanges' flag.
2. **Run the Backend**  
   - Open a terminal in the `VituraHealth.PrescriptionManagement.Server` directory.
   - Run the following command to restore dependencies and build the project: dotnet restore
   - Then, run the project with: `dotnet run --project VituraHealth.PrescriptionManagement.Server` or use Visual Studio to run the project.

The API will be available at `http://localhost:5299`.


### To Run the Client / Frontend (React + Vite)
1. **Install dependencies**  
   Open a terminal in the `Viturahealth.prescriptionmanagement.client` directory and run: `npm install`
   
2. **Set the API base URL if needed**
  VITE_API_BASE=http://localhost:5299/api

3. **Proxy Setup** 
   The frontend is configured to proxy API requests to the backend. Ensure both servers are running
    
4. **Run the Frontend**  
   In the same terminal, run: `npm run dev`

The app will be available at `http://localhost:5300` (or as indicated in the terminal).

## 2. Running unit and component tests
1. **Run the Frontend component tests**  
   - Open a terminal in the `Viturahealth.prescriptionmanagement.client`
   - In the same terminal, run: `npm run test`
2. **Run the Backend unit tests**  
   - Open a terminal in the `VituraHealth.PrescriptionManagement.Server.Tests` directory.
   - Run the following command to restore dependencies and build the project: `dotnet restore`
   - Then, run the tests with: `dotnet test` 


## 3. Assumptions or Shortcuts Made

- **Data Storage:**  
  Patient and prescription data are stored in JSON files for simplicity. No database is used, however **there is a flag in the appsettings file to turn persistence of prescription data to the data file, as needed**.
- **Authentication:**  
  No authentication or authorization is implemented.
- **Error Handling:**  
  Basic error handling is provided; more robust validation and error reporting may be needed for production.
- **CORS:**  
  CORS is configured to allow requests from `https://localhost:63845` (update as needed).
- **Frontend/Backend Communication:**  
  The frontend expects the backend to be running locally with HTTPS enabled.

---

## 4. Known Limitations

- **Scalability:**  
  JSON file storage is not suitable for large-scale or concurrent usage.
- **Security:**  
  No user authentication, authorization, or data encryption.
- **Validation:**  
  Minimal validation is performed on both frontend and backend.
- **Deployment:**  
  The project is set up for local development only. Additional configuration is required for production deployment.
- **Browser Compatibility:**  
  Tested primarily in modern browsers; older browsers may have issues.

---

## Additional Notes

- **Swagger UI:**  
  API documentation is available at `/swagger` when running in development mode.
- **Styling:**  
  The frontend uses Tailwind CSS for modern, responsive design.
- **Toast Notifications:**  
  Success and error messages are shown using React Toastify.

---
## Troubleshooting
- If you see errors about missing data, ensure your JSON files exist and are correctly referenced in `appsettings.json`.
- If you encounter port conflicts, make sure no other process is using port 5299 or 5300.


For any questions or issues, please refer to the code comments or contact **Steve Kaminski**.
