//Arrumar depois, modal para deletar item com validação

export default function ModalDelete() {
    return (
            <div className={styles.modal}>
                <div className={styles.modalContent}>
                    <h3>Confirmar Exclusão</h3>
                    <p>Você quer excluir permanentemente {productToDelete?.title}?</p>
                    <button onClick={handleDeleteConfirm} className={styles.modalButton}>Sim</button>
                    <button onClick={() => setShowDeleteModal(false)} className={styles.modalButtonCancel}>Não</button>
                </div>
            </div>
    )

}