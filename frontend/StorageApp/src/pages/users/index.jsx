import { useState, useMemo, useEffect } from "react";
import { Plus } from "lucide-react";
import UserRow from "../../components/layout/UserRow";
import Pagination from "../../components/ui/Pagination";
import styles from "../../styles/pages/usuarios.module.css";
import { useToast } from "../../hooks/useToast";
import { useFetchApi } from "../../hooks/useFetchApi";
import { useMutateApi } from "../../hooks/useMutateApi";
import { useAuth } from "../../contexts/AuthContext";

const ITEMS_PER_PAGE = 10;

export default function Users() {
    const [searchInput, setSearchInput] = useState("");
    const [currentPage, setCurrentPage] = useState(1);

    const toast = useToast();
    const { user: currentUser } = useAuth();

    const {
        data: usersData,
        loading,
        error,
        refetch: refetchUsers
    } = useFetchApi("User.UserGetAll");

    const { mutate: mutateDelete } = useMutateApi("User.UserDelete");

    const users = useMemo(() => {
        console.log("Dados de usuários recebidos:", usersData);
        if (Array.isArray(usersData)) return usersData;
        if (usersData?.items && Array.isArray(usersData.items))
            return usersData.items;
        if (usersData?.value && Array.isArray(usersData.value))
            return usersData.value;
        return [];
    }, [usersData]);

    const filteredUsers = useMemo(() => {
        const search = searchInput.toLowerCase().trim();
        if (!search) return users;

        return users.filter((user) =>
            [user.name, user.email, user.role]
                .join(" ")
                .toLowerCase()
                .includes(search)
        );
    }, [users, searchInput]);

    const totalPages = Math.ceil(filteredUsers.length / ITEMS_PER_PAGE);

    const currentUsers = useMemo(() => {
        const start = (currentPage - 1) * ITEMS_PER_PAGE;
        return filteredUsers.slice(start, start + ITEMS_PER_PAGE);
    }, [filteredUsers, currentPage]);


    useEffect(() => {
        if (currentPage > totalPages && totalPages > 0) {
            setCurrentPage(totalPages);
        }
    }, [totalPages, currentPage]);

    useEffect(() => {
        setCurrentPage(1);
    }, [searchInput]);

    const handleDelete = async (id) => {
        const user = users.find((u) => u.id === id);

        if (!window.confirm(`Tem certeza que deseja deletar ${user?.name}?`)) {
            return;
        }

        mutateDelete(
            { id },
            {
                onSuccess: () => {
                    toast.success("Usuário deletado com sucesso!");
                    refetchUsers();
                },
                onError: (err) => {
                    toast.error(
                        `Erro ao deletar usuário: ${err?.message || "Erro desconhecido"}`
                    );
                },
            }
        );
    };

    const handleEdit = (id) => {
        const user = users.find((u) => u.id === id);
        toast.info(`Editar ${user?.name} será implementado em breve`);
    };

    const handleAddUser = () => {
        toast.info("Funcionalidade de criação será implementada em breve");
    };

    if (error && loading === false) {
        console.error("Erro ao buscar usuários:", error);
    }

    return (
        <div className={styles.pageWrapper}>
            <div className={styles.container}>
                <div className={styles.header}>
                    <h2>
                        {filteredUsers.length} usuários cadastrados
                    </h2>

                    <div className={styles.headerActions}>
                        <input
                            type="text"
                            placeholder="Nome, Email ou Função"
                            className={styles.search}
                            value={searchInput}
                            onChange={(e) => setSearchInput(e.target.value)}
                        />

                        {currentUser?.role === "Admin" && (
                            <button
                                className={styles.addUser}
                                onClick={handleAddUser}
                            >
                                <Plus size={16} />
                                Novo Usuário
                            </button>
                        )}
                    </div>
                </div>

                {loading ? (
                    <div className={styles.loading}>
                        Carregando usuários...
                    </div>
                ) : filteredUsers.length === 0 ? (
                    <div className={styles.emptyState}>
                        Nenhum usuário encontrado
                    </div>
                ) : (
                    <>
                        <table className={styles.usersTable}>
                            <thead>
                                <tr>
                                    <th />
                                    <th>Nome</th>
                                    <th>Email</th>
                                    <th>Função</th>
                                    <th>Atendimento</th>
                                    <th>Callcenter</th>
                                    <th>Técnico</th>
                                    <th>Vendedor</th>
                                    <th>2FA</th>
                                    <th>Ativo</th>
                                    <th>Ações</th>
                                </tr>
                            </thead>
                            <tbody>
                                {currentUsers.map((user) => (
                                    <UserRow
                                        key={user.id}
                                        user={user}
                                        onEdit={handleEdit}
                                        onDelete={handleDelete}
                                    />
                                ))}
                            </tbody>
                        </table>

                        {totalPages > 1 && (
                            <Pagination
                                currentPage={currentPage}
                                totalPages={totalPages}
                                onChange={setCurrentPage}
                            />
                        )}
                    </>
                )}
            </div>
        </div>
    );
}