import { useState } from 'react'
import Login from './Login'
import Registro from './Registro'
import './Auth.css'

type Modo = 'login' | 'registro'

function Auth() {
  const [modo, setModo] = useState<Modo>('login')

  return (
    <main className="auth">
      <aside className="auth__marca">
        <div className="auth__logo">
          <span className="auth__logo-icono" aria-hidden="true" />
          Tickets
        </div>
        <div>
          <h1>Cada solicitud, bajo control.</h1>
          <p>
            Crea, asigna y da seguimiento a tus tickets de soporte desde un solo lugar.
          </p>
        </div>
        <ul className="auth__puntos">
          <li>Seguimiento en tiempo real</li>
          <li>Asignación a tu equipo de soporte</li>
          <li>Acceso por roles</li>
        </ul>
      </aside>

      <section className="auth__panel">
        <div className="auth__tarjeta">
          <div className="auth__tabs" role="tablist">
            <button
              type="button"
              role="tab"
              aria-selected={modo === 'login'}
              className={modo === 'login' ? 'activo' : ''}
              onClick={() => setModo('login')}
            >
              Iniciar sesión
            </button>
            <button
              type="button"
              role="tab"
              aria-selected={modo === 'registro'}
              className={modo === 'registro' ? 'activo' : ''}
              onClick={() => setModo('registro')}
            >
              Registrarse
            </button>
          </div>

          {modo === 'login' ? (
            <Login irARegistro={() => setModo('registro')} />
          ) : (
            <Registro irALogin={() => setModo('login')} />
          )}
        </div>
      </section>
    </main>
  )
}

export default Auth
