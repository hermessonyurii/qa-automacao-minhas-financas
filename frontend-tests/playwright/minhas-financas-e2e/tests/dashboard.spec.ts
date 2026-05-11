import { expect, test } from "@playwright/test";

test.describe("Dashboard - Minhas Finanças", () => {
  test("deve carregar o dashboard sem erro visível", async ({ page }) => {
    await page.goto("/");

    await expect(page.locator("body")).toBeVisible();

    const bodyText = await page.locator("body").innerText();

    expect(bodyText.trim().length).toBeGreaterThan(0);
  });

  test("não deve apresentar página em branco ao carregar a aplicação", async ({ page }) => {
    await page.goto("/");

    const bodyText = await page.locator("body").innerText();

    expect(bodyText.trim().length).toBeGreaterThan(0);
  });
});