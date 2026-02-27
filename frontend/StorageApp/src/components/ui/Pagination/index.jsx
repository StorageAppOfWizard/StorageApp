import React from 'react';
import styles from './pagination.module.css';

export default function Pagination({
  currentPage = 1,
  totalPages = 1,
  onChange,
  className = '',
}) {
  if (totalPages <= 1) return null;

  const pages = Array.from({ length: totalPages }, (_, i) => i + 1);

  const handleClick = (page) => {
    if (page !== currentPage && onChange) {
      onChange(page);
    }
  };

  return (
    <div className={`${styles.container} ${className}`}>      
      <button
        className={styles.button}
        disabled={currentPage === 1}
        onClick={() => handleClick(1)}
        title="Primeira página"
      >
        «
      </button>

      <button
        className={styles.button}
        disabled={currentPage === 1}
        onClick={() => handleClick(currentPage - 1)}
        title="Página anterior"
      >
        ‹
      </button>

      {pages.map((page) => {
        // mostrar apenas algumas páginas próximas
        if (
          page === 1 ||
          page === totalPages ||
          (page >= currentPage - 1 && page <= currentPage + 1)
        ) {
          return (
            <button
              key={page}
              className={`${styles.button} ${
                page === currentPage ? styles.active : ''
              }`}
              onClick={() => handleClick(page)}
            >
              {page}
            </button>
          );
        }
        if (
          page === currentPage - 2 ||
          page === currentPage + 2
        ) {
          return <span key={page} className={styles.info}>...</span>;
        }
        return null;
      })}

      <button
        className={styles.button}
        disabled={currentPage === totalPages}
        onClick={() => handleClick(currentPage + 1)}
        title="Próxima página"
      >
        ›
      </button>

      <button
        className={styles.button}
        disabled={currentPage === totalPages}
        onClick={() => handleClick(totalPages)}
        title="Última página"
      >
        »
      </button>
    </div>
  );
}
