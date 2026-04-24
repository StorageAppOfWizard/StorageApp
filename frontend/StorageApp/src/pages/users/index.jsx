import { useState, useMemo, useEffect } from "react";
import { Plus, Search } from "lucide-react";

// Componentes novos (específicos da feature de users)
import UserStatsCards from "../../components/features/UserStatsCards";
import UserRoleFilter from "../../components/features/UserRoleFilter";
import UserCard from "../../components/features/UserCard";

// Componentes já existentes
import Pagination from "../../components/ui/Pagination";
import EditUserModal from "../../components/features/EditUserModal";

import styles from "../../styles/pages/usuarios.module.css";
import { useToast } from "../../hooks/useToast";
import { useFetchApi } from "../../hooks/useFetchApi";
import { useMutateApi } from "../../hooks/useMutateApi";
import { useAuth } from "../../contexts/AuthContext";
import { usePagination } from "../../hooks/usePagination";

const ITEMS_PER_PAGE = 12;
 
function normalizeUsers(data) {
  if (Array.isArray(data)) return data;
  if (data?.items) return data.items;
  if (data?.value) return data.value;
  return [];
}
 
/**
 * getRoleValue
 *
 * A API retorna role como array: ["Admin"].
 * Esta função normaliza para string em qualquer lugar que precisarmos comparar.
 * Centralizar aqui evita repetir Array.isArray em vários lugares.
 */
function getRoleValue(role) {
  return Array.isArray(role) ? role[0] : role;
}
 
/**
 * calculateStats
 *
 * Compara com o valor já normalizado via getRoleValue.
 * Assim "Admin" (capitalizado, como a API manda) bate certo.
 */
function calculateStats(users) {
  return users.reduce(
    (acc, user) => {
      acc.total += 1;
      const role = getRoleValue(user.role);
      if (role === "Admin") acc.admin += 1;
      else if (role === "Manager") acc.manager += 1;
      else acc.member += 1;
      return acc;
    },
    { total: 0, admin: 0, manager: 0, member: 0 }
  );
}
 
export default function Users() {
  const [searchInput, setSearchInput] = useState("");
  const [roleFilter, setRoleFilter] = useState("all");
  const [editingUser, setEditingUser] = useState(null);
 
  const toast = useToast();
  const { user: currentUser } = useAuth();
 
  const {
    data: usersData,
    loading,
    error,
    refetch: refetchUsers,
  } = useFetchApi("User.UserGetAll");
 
  const { mutate: mutateDelete } = useMutateApi("User.UserDelete");
 
  const users = useMemo(() => normalizeUsers(usersData), [usersData]);
 
  const usersMap = useMemo(
    () => Object.fromEntries(users.map((u) => [u.id, u])),
    [users]
  );
 
  const stats = useMemo(() => calculateStats(users), [users]);
 
  /**
   * filteredUsers
   *
   * Filtro combinado: role + texto.
   *
   * Para o role: normalizamos com getRoleValue antes de comparar.
   * Para o texto: buscamos em `userName` (campo real da API).
   */
  const filteredUsers = useMemo(() => {
    const search = searchInput.toLowerCase().trim();
    const terms = search ? search.split(" ") : [];
 
    return users.filter((user) => {
      const role = getRoleValue(user.role);
 
      if (roleFilter !== "all" && role !== roleFilter) return false;
 
      if (terms.length > 0) {
        const userName = user.userName?.toLowerCase() || "";
        const email = user.email?.toLowerCase() || "";
        if (!terms.every((t) => userName.includes(t) || email.includes(t))) {
          return false;
        }
      }
 
      return true;
    });
  }, [users, searchInput, roleFilter]);
 
  const {
    currentPage,
    setCurrentPage,
    totalPages,
    paginatedData: currentUsers,
  } = usePagination(filteredUsers, ITEMS_PER_PAGE);
 
  useEffect(() => {
    if (currentPage > totalPages) setCurrentPage(totalPages);
  }, [totalPages, currentPage]);
 
  const handleSearch = (e) => {
    setSearchInput(e.target.value);
    setCurrentPage(1);
  };
 
  const handleRoleFilter = (role) => {
    setRoleFilter(role);
    setCurrentPage(1);
  };
 
  const handleDelete = async (id) => {
    const user = usersMap[id];
    if (!window.confirm(`Tem certeza que deseja deletar ${user?.userName}?`)) return;
 
    mutateDelete(
      { id },
      {
        onSuccess: () => {
          toast.success("Usuário deletado com sucesso!");
          refetchUsers();
        },
        onError: (err) => {
          toast.error(`Erro ao deletar usuário: ${err?.message || "Erro desconhecido"}`);
        },
      }
    );
  };
 
  const handleEdit = (id) => setEditingUser(usersMap[id]);
 
  const handleSave = () => refetchUsers();
 
  const handleAddUser = () => {
    toast.info("Funcionalidade de criação será implementada em breve");
  };
 
  const isAdmin = getRoleValue(currentUser?.role) === "Admin";
 
  useEffect(() => {
    if (error) console.error("Erro ao buscar usuários:", error);
  }, [error]);
 
  return (
    <div className={styles.pageWrapper}>
      <div className={styles.container}>
 
        {/* ── Cabeçalho ── */}
        <div className={styles.header}>
          <div className={styles.headerLeft}>
            <h2>Gerenciamento de Usuários</h2>
            <p>{users.length} usuários cadastrados</p>
          </div>
 
          {isAdmin && (
            <button className={styles.addUser} onClick={handleAddUser}>
              <Plus size={16} />
              Novo Usuário
            </button>
          )}
        </div>
 
        {/* ── Cards de estatísticas ── */}
        <UserStatsCards stats={stats} />
 
        {/* ── Toolbar: busca + filtros de role ── */}
        <div className={styles.toolbar}>
          <div className={styles.searchWrapper}>
            <Search size={15} className={styles.searchIcon} />
            <input
              type="text"
              placeholder="Buscar por nome ou email..."
              className={styles.search}
              value={searchInput}
              onChange={handleSearch}
            />
          </div>
 
          <UserRoleFilter activeRole={roleFilter} onChange={handleRoleFilter} />
        </div>
 
        {/* ── Conteúdo principal ── */}
        {loading ? (
          <div className={styles.loading}>Carregando usuários...</div>
        ) : filteredUsers.length === 0 ? (
          <div className={styles.emptyState}>
            <strong>Nenhum usuário encontrado</strong>
            Tente ajustar os filtros ou a busca.
          </div>
        ) : (
          <>
            <div className={styles.cardsGrid}>
              {currentUsers.map((user) => (
                <UserCard
                  key={user.id}
                  user={user}
                  onEdit={handleEdit}
                  onDelete={handleDelete}
                />
              ))}
            </div>
 
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
 
      {editingUser && (
        <EditUserModal
          user={editingUser}
          onClose={() => setEditingUser(null)}
          onSave={handleSave}
        />
      )}
    </div>
  );
}