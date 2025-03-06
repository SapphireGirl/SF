import { useEffect, useState } from 'react';
import { Log } from './logger';
import configData from "./logConfig.json";

import { DataGrid, GridColDef, GridRowHeightParams } from '@mui/x-data-grid';
import Box from '@mui/material/Box';
import { SimpleTreeView } from '@mui/x-tree-view/SimpleTreeView';
import { TreeItem } from '@mui/x-tree-view/TreeItem';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import Typography from '@mui/material/Typography';

import { formatCurrency } from './helpers/Formatters';
import './App.css';

interface Home {
   
    id: number;
    address: string;
    city: string;
    state: string;
    zipcode: number,
    comments: string;
    url: string;
    price: string;
}

function App() {

    const [homes, setHomes] = useState<Home[]>([]);
    const [pageSize, setPageSize] = useState<number>(5);
    const [page, setPage] = useState<number>(0);
    const [expandedRows, setExpandedRows] = useState<Set<number>>(new Set());

    useEffect(() => {

        const getHomes = async () => {
            try {
                const homesData = await populateHomeData();

                if (homesData.length != 0) {
                    const homesWithPrices = homesData.map(home => ({
                        ...home,
                        // This is probably not the best place to do this, but it's a quick fix for now
                        price: formatCurrency(parseInt(home.price))
                    }));

                    setHomes(homesWithPrices);
                    const logData = JSON.stringify(homesWithPrices);
                    const log = new Log('getHomes \n' +logData);
                    log.info();
                }

            }
            catch (error) {
                const logData = JSON.stringify(error);
                const log = new Log(`Error ${error}: ${logData}`);
                log.error();
            }

        };
        getHomes();
    }, []);

    const handleRowClick = (id: number) => {
        setExpandedRows(prev => {
            const newExpandedRows = new Set(prev);
            if (newExpandedRows.has(id)) {
                newExpandedRows.delete(id);
            } else {
                newExpandedRows.add(id);
            }
            return newExpandedRows;
        });
    };

    const getRowHeight = (params: GridRowHeightParams) => {
        return expandedRows.has(params.id as number) ? 200 : 52;
    };

    const theme = createTheme(
        {
            palette: {
                mode: 'dark',
                primary: {
                    main: '#3f51b5',
                },
                secondary: {
                    main: '#f50057',
                },
                background: {
                    paper: '#7d0e73',
                    default: '#3b0241',
                },
                
                //pink: {
                //    deep: '#FF1493',
                //    hot: '#FF69B4',
                //    medium: '#C71585',
                //    pale: '#DB7093',
                //    light: '#FFB6C1',
                //},
            },
        },
    );

    const columns: GridColDef[] = [        {
            field: '',
            //headerName: 'Details',
            width: 50,
            renderCell: (params) => (
                <Box onClick={() => handleRowClick(params.id as number)}
                    className={expandedRows.has(params.id as number) ? 'expanded-row' : ''}
                >
                    <SimpleTreeView multiSelect>
                        <TreeItem itemId={`grid-${params.id}`} key={params.id}>
                            <div>
                                {params.row.address}
                            </div>
                            <div>
                                {params.row.city}
                            </div>
                            <div>
                                {params.row.state}
                            </div>
                        </TreeItem>
                    </SimpleTreeView>
                </Box>
            )
        },
        { field: 'id', headerName: 'ID', width: 90 },
        { field: 'address', headerName: 'Address', width: 150 },
        { field: 'city', headerName: 'City', width: 150 },
        { field: 'state', headerName: 'State', width: 110 },
        { field: 'price', headerName: 'Price', width: 130 },
        { field: 'url', headerName: 'Url', width: 200 }
    ];



    return (
        <div style={{ height: 400, width: '100%' }} >
            <Typography id="tabelLabel">Santa Fe Homes</Typography>
            
            <ThemeProvider theme={theme}>
            <DataGrid
                
                rows={homes}
                columns={columns}
                paginationModel={{ pageSize, page }}
                onPaginationModelChange={(model) => {
                    setPageSize(model.pageSize);
                    setPage(model.page);
                }}
                pageSizeOptions={[5, 10, 20]}
                getRowHeight={getRowHeight}
                sx={{
                    color: '#bb0fd6',
                }}
                //enableExpanding:true
                />
            </ThemeProvider>
        </div>
    );

    async function populateHomeData(): Promise<Home[]>{

        const baseUrl = configData.development.apiUrl;

        const response = await fetch(`${baseUrl}/api/home/GetAllAsync`);
 
        if (!response.ok) {
            const logData = JSON.stringify(response);
            const log = new Log(`This is the error ${logData}`);
            log.error();
            throw new Error('Network response was not ok');
        }
        else {
            const logData = JSON.stringify(response);
            const log = new Log('populateHomeData ' + logData);
            log.info();
            return response.json();
        }
    }
}

export default App;