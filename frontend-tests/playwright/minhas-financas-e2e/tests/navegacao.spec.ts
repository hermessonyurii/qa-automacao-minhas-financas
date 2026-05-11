import { expect, test } from "@playwright/test";

test.describe("Navegação principal - Minhas Finanças", () => {
  test("deve carregar a aplicação com sucesso", async ({ page }) => {
    await page.goto("/");

    await expect(page).toHaveURL(/localhost:5173/);
    await expect(page.locator("body")).toBeVisible();
  });

  test("deve acessar a tela de Pessoas", async ({ page }) => {
    await page.goto("/pessoas");

    await expect(page.locator("body")).toBeVisible();
    await expect(page).toHaveURL(/pessoas/);
  });

  test("deve acessar a tela de Categorias", async ({ page }) => {
    await page.goto("/categorias");

    await expect(page.locator("body")).toBeVisible();
    await expect(page).toHaveURL(/categorias/);
  });

  test("deve acessar a tela de Transações", async ({ page }) => {
    await page.goto("/transacoes");

    await expect(page.locator("body")).toBeVisible();
    await expect(page).toHaveURL(/transacoes/);
  });
});