import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import './index.css'
import { DataGridTest } from './DataGridTest.tsx'
import { Download } from './Download.tsx'

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <Download />
  </React.StrictMode>,
)
