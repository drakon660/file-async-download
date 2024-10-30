import React, { useEffect, useState } from 'react';
import { GridColDef, GridPaginationModel } from '@mui/x-data-grid';
import axios from 'axios';
import { DataGridPro } from '@mui/x-data-grid-pro/DataGridPro';

export const DataGridTest = ()=>{
    const [data, setData] = useState([]);
    const [page, setPage] = useState(0);
    const [pageSize, setPageSize] = useState(10);
    const [rowCount, setRowCount] = useState(200); // Set to total number of items from API
  

    const fetchData = async (page: number, pageSize: number) => {
      try {
        const response = await axios.get(
          `http://localhost:5042/data?page=${page + 1}&pageSize=${pageSize}`
        );
        setData(response.data);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };
  
    useEffect(() => {
      fetchData(page, pageSize);
    }, [page, pageSize]);
  
    const handlePaginationModelChange = (model: GridPaginationModel) => {
      setPage(model.page);
      setPageSize(model.pageSize);
    };
  
    const columns: GridColDef[] = [
      { field: 'id', headerName: 'ID', width: 100 },
      { field: 'content', headerName: 'Content', width: 300 }
    ];
  
    return (
      <div style={{ height: 800, width: '100%' }}>
        <DataGridPro        
          rows={data}
          pagination
          columns={columns}                   
          paginationMode="server"          
          rowCount={rowCount}          
          pageSizeOptions={[10, 25, 50, 100]}
          onPaginationModelChange={handlePaginationModelChange}
        />
      </div>
    );
}