// Para operações de leitura (GET)
import {useState, useEffect} from "react";
import axios from "axios";
import { endpointMap } from "../endpoints";

export const useFetchApi = (endpoint) => {

    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null)

    useEffect(() => {
        const controller = new AbortController();
        let isMounted = true;

        const fetchData = async () =>{
            try{
                setLoading(true);
                const config = endpointMap[endpoint];
                if (!config) {
                    throw new Error("Endpoint não suporta");
                }

                const response = await config.fn(controller.signal)
                if(isMounted && response) {
                    const transformData = config.transform ? config.transform(response) : response;
                    setData(transformData);
                    console.log(`Response para ${endpoint}:`, response);
                }
            } catch (error) {
                if(!axios.isCancel(error) && isMounted) {
                    setError(`Erro ao carregar dados: ${error.message}`);
                }
            } finally {
                if(isMounted) setLoading(false);
            }
        };
        fetchData();

        return() =>{
            isMounted = false;
            controller.abort();
        };
    }, [endpoint]);

    return {data, loading, error};
};
