import Skeleton from "react-loading-skeleton";
import "react-loading-skeleton/dist/skeleton.css";
import styles from "../../styles/produtos.module.css";

const ProductTableSkeleton = () => {
  return (
    <div style={{ marginTop: "60px", padding: "20px" }}>
      <div className={styles.skeletonContainer}>
        <table className={styles.productTable}>
          <thead>
            <tr>
              <th><Skeleton height={20} /></th>
              <th><Skeleton height={20} /></th>
              <th><Skeleton height={20} /></th>
              <th><Skeleton height={20} /></th>
              <th><Skeleton height={20} /></th>
              <th><Skeleton height={20} /></th>
              <th><Skeleton height={20} /></th>
            </tr>
          </thead>
          <tbody>
            {Array(5).fill().map((_, index) => (
              <tr key={index}>
                <td><Skeleton height={50} width={50} /></td>
                <td><Skeleton height={20} width={150} /></td>
                <td><Skeleton height={20} width={120} /></td>
                <td><Skeleton height={20} width={120} /></td>
                <td><Skeleton height={20} width={80} /></td>
                <td><Skeleton height={20} width={50} /></td>
                <td><Skeleton height={20} width={80} /></td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default ProductTableSkeleton;