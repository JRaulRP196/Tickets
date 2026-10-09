import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { AuthProvider } from "./Servicios/Context.tsx";
import "./index.css";
import App from "./App.tsx";
import { BrowserRouter } from "react-router";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <BrowserRouter>
      <AuthProvider>
        <App />
      </AuthProvider>
    </BrowserRouter>
  </StrictMode>,
);
