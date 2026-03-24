import { useState, useMemo, useEffect } from "react";
import { Plus } from "lucide-react";
import UserRow from "../../components/layout/UserRow";
import Pagination from "../../components/ui/Pagination";
import styles from "../../styles/pages/usuarios.module.css";
import { useToast } from "../../hooks/useToast";
import { useFetchApi } from "../../hooks/useFetchApi";
import { useMutateApi } from "../../hooks/useMutateApi";
import { useAuth } from "../../contexts/AuthContext";
import { usePagination } from "../../hooks/usePagination";

const ITEMS_PER_PAGE = 10;

function normalizeUsers(data) {
    if (Array.isArray(data)) return data;
    if (data?.items) return data.items;
    if (data?.value) return data.value;
    return [];
}

export default function Users() {
    const [searchInput, setSearchInput] = useState("");

    const toast = useToast();
    const { user: currentUser } = useAuth();

    const {
        data: usersData,
        loading,
        error,
        refetch: refetchUsers
    } = useFetchApi("User.UserGetAll");

    const { mutate: mutateDelete } = useMutateApi("User.UserDelete");

    const users = useMemo(() => normalizeUsers(usersData), [usersData]);

    const usersMap = useMemo(() => {
        return Object.fromEntries(users.map((u) => [u.id, u]));
    }, [users]);

    const filteredUsers = useMemo(() => {
        const search = searchInput.toLowerCase().trim();
        if (!search) return users;

        return users.filter((user) => {
            return (
                user.name?.toLowerCase().includes(search) ||
                user.email?.toLowerCase().includes(search) ||
                user.role?.toLowerCase().includes(search)
            );
        });
    }, [users, searchInput]);

    const {
        currentPage,
        setCurrentPage,
        totalPages,
        paginatedData: currentUsers
    } = usePagination(filteredUsers, ITEMS_PER_PAGE);

    useEffect(() => {
        if (currentPage > totalPages) {
            setCurrentPage(totalPages);
        }
    }, [totalPages, currentPage]);

    const handleSearch = (e) => {
        setSearchInput(e.target.value);
        setCurrentPage(1);
    };

    const handleDelete = async (id) => {
        const user = usersMap[id];

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
        const user = usersMap[id];
        toast.info(`Editar ${user?.name} será implementado em breve`);
    };

    const handleAddUser = () => {
        toast.info("Funcionalidade de criação será implementada em breve");
    };

    const isAdmin = currentUser?.role === "Admin";

    useEffect(() => {
        if (error) {
            console.error("Erro ao buscar usuários:", error);
        }
    }, [error]);

    return (
        <div className={styles.pageWrapper}>
            <div className={styles.container}>
                <div className={styles.header}>
                    <h2>{filteredUsers.length} usuários cadastrados</h2>

                    <div className={styles.headerActions}>
                        <input
                            type="text"
                            placeholder="Nome, Email ou Função"
                            className={styles.search}
                            value={searchInput}
                            onChange={handleSearch}
                        />

                        {isAdmin && (
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
                                    <th>Status</th>
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