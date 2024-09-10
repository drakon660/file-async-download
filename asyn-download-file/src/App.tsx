import { Autocomplete, MenuItem, Select, TextField } from '@mui/material';
import './App.css';
import { Download } from './Download';
import top100Films from './top100Films';

function App() {

  return (
    <>
      <Download></Download>  
      <Select value={top100Films[0].label}  MenuProps={{
         PaperProps: {
          sx: {          
           '& .MuiPopover-root': {
                position: 'static !important'
            },
            '& .MuiMenu-root': {
                position: 'static !important'
            },
            '& .MuiModal-root': {
                position: 'static !important'
            },
          },
        },
      }}>
        { 
          top100Films.map((item)=><MenuItem value={item.label}>
           {item.label}
          </MenuItem>)
        }     
      </Select>
      <Autocomplete
      disablePortal
      options={top100Films}
      disableClearable
      sx={{ width: 300 }}
      style={{
        cursor:'none'
      }}
      renderInput={(params) => (
        <TextField
          {...params}
          label="Movie"
          InputProps={{
            style: {   
              caretColor: 'transparent',
              cursor:'none',           
              userSelect: 'none', // Disable text selection             // Disable pointer events to prevent cursor inside input
            },
            ...params.InputProps,
            readOnly: true, // Prevent typing
          }}
        />
      )}
      onMouseDown={(e) => e.preventDefault()}
      onKeyDown={(event) => {
        event.preventDefault(); // Prevent keyboard interaction
      }}
      // Optional: Control when the dropdown opens
      openOnFocus={false} // Prevent dropdown on focus
    />
    </>
  )
}

export default App
