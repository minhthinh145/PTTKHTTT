interface Props {
  value: string;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

export const UsernameField = ({ value, onChange }: Props) => {
  return (
    <div className="relative w-full max-w-md mx-auto mb-6">
      <input
        id="username"
        type="text"
        className="peer block w-full rounded-lg border-2 border-blue-800  px-4 pt-6 pb-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-600"
        placeholder=" "
        value={value}
        onChange={onChange}
      />
      <label
        htmlFor="username"
        className="absolute left-3 top-1 text-xs text-blue-800 transition-all peer-placeholder-shown:top-4 peer-placeholder-shown:text-sm peer-placeholder-shown:text-gray-500 peer-focus:top-1 peer-focus:text-xs peer-focus:text-blue-800"
      >
        Tên đăng nhập
      </label>
    </div>
  );
};
