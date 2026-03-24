//Reutilizavel para paginação
import { useState, useMemo, useEffect } from "react";

export function usePagination(data = [], itemsPerPage = 10) {
  const [currentPage, setCurrentPage] = useState(1);

  const totalPages = Math.max(1, Math.ceil(data.length / itemsPerPage));

  const paginatedData = useMemo(() => {
    const start = (currentPage - 1) * itemsPerPage;
    return data.slice(start, start + itemsPerPage);
  }, [data, currentPage, itemsPerPage]);

  useEffect(() => {
    if (currentPage > totalPages) {
      setCurrentPage(totalPages);
    }
  }, [currentPage, totalPages]);

  return {
    currentPage,
    setCurrentPage,
    totalPages,
    paginatedData,
  };
}
